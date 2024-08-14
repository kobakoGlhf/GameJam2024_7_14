using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UiTimeLimit))]
public class Choices : MonoBehaviour
{
    [SerializeField, Tooltip("一回あたりの制限時間")] float _limitedTime;
    [SerializeField, Tooltip("選択肢")] public string[] _choices;
    [SerializeField] Text _textW;
    [SerializeField] Text _textA;
    [SerializeField] Text _textS;
    [SerializeField] Text _textD;
    [SerializeField] GameObject _goodEffect;
    [SerializeField] GameObject _badEffect;
    [SerializeField] Slider _timeSlider;
    Score _score;
    [SerializeField] ChoiceEffect _choiceEffect;
    public string[] _selectedChoices = new string[3];
    public bool _inGame;//名前変更、ゲーム中かどうかのフラグ
    [SerializeField] public GameObject _question;
    TalkText _talkText;
    [SerializeField] GameObject _timerPrefab;
    UiTimeLimit _timeLimit;
    [SerializeField] RectTransform _timerRectTransform;
    [SerializeField] ChatManager _chatManager;
    [SerializeField] ChoicesMoveManager _choiceMoveManager;
    FacialExpression _faialExpression;
    int _currentScore;
    public string[,,] _choicesNeo;//choicesを使いやすく格納するための配列。      左からloop回数、選択回数、選択結果
    int _loopCount;//ループ回数。
    public int _choicesCount;//1ループごとにの選択できる回数
    Coroutine _coroutineTimer;//null入れるよう
    bool _duringAnimation;
    int[] _randomKeyArray;
    [SerializeField]AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    private void Start()
    {
        _score = GameObject.FindObjectOfType<Score>();
        _timeLimit = GameObject.FindObjectOfType<UiTimeLimit>().GetComponent<UiTimeLimit>();
        _talkText = GetComponent<TalkText>();
        _question.gameObject.SetActive(false);
        _faialExpression = GetComponent<FacialExpression>();
        enabled = false;
    }

    public void TimerStart()
    {
        _coroutineTimer = StartCoroutine(ChoicesTimer());//fix
        _timeLimit.Display(_timerPrefab, _limitedTime, new Vector2(_timerRectTransform.position.x, _timerRectTransform.position.y), _timerRectTransform.gameObject);
    }
    private void Update()
    {
        if (_inGame&&_duringAnimation==false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChoiceButton(0);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                ChoiceButton(1);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ChoiceButton(2);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ChoiceButton(3);
            }
        }
    }
    public void ChoiceButton(int sum)// EventTriggerで呼び出せるようにめんどくさいことしてます。
    {
        UiTimeLimit._timerStop = false;
        if (_inGame && _duringAnimation == false)
        {
            char key;
            switch (sum)
            {
                case 0:
                    sum = _randomKeyArray[0];
                    key = 'W';
                    break;
                case 1:
                    sum = _randomKeyArray[1];
                    key = 'A';
                    break;
                case 2:
                    sum = _randomKeyArray[2];
                    key = 'S';
                    break;
                case 3:
                    sum = _randomKeyArray[3];
                    key = 'D';
                    break;
                default:
                    sum = Random.Range(0, _randomKeyArray.Length - 1);
                    key = ' ';
                    break;
            }
            _choiceMoveManager.SelectKey(key);
            StopCoroutine(ChoicesTimer());
            _coroutineTimer = null;//fix コルーチンリセット
            _selectedChoices[_choicesCount] = _choicesNeo[_loopCount, _choicesCount, sum]; //選択肢を保存
            _choicesCount++; //カウントを増やす
            StartCoroutine(ChoiceEffect(_choicesCount == 3)); 
            _audioSource.PlayOneShot(_audioClip);
        }
    }
    public IEnumerator ChoicesTimer()   //選択のタイマー //fix 挙動の修正
    {
        float timer = 0;
        int count = _choicesCount;
        _selectedChoices[count] = "";
        while (timer <= _limitedTime)
        {
            if (_selectedChoices[count] == "")
            {
                yield return null;
            }
            else
            {
                yield break;
            }
            timer += Time.deltaTime;
        }
        int random = Random.Range(0, 3);
        ChoiceButton(-1);
    }
    IEnumerator ChoiceEffect(bool nextLoop)
    {
        _duringAnimation = true;
        yield return new WaitForSeconds(_choiceMoveManager._animetionTimer / 2);
        if (!nextLoop)
        {
            ChoiceDisplay(); //表示を変更する
            yield return new WaitForSeconds(_choiceMoveManager._animetionTimer / 2);
            _timeLimit.DestroyTimer();
            TimerStart();
        }
        else //loop終了時の処理
        {
            _timeLimit.DestroyTimer();
            _talkText.enabled = true;
            _talkText._text.text = "";
            _talkText.TextActive();
            _question.gameObject.SetActive(false);
            enabled = false;
            _currentScore = Score._score;
            _score.AddScore();
            //パーティクルシステムによるエフェクト再生部分
            _choiceEffect.AnimationPlay(_selectedChoices[0] + _selectedChoices[1] + _selectedChoices[2]);//表示する文字列を入れる
            if (_currentScore != Score._score)
            {
                _faialExpression.Correct();
                StartCoroutine(GoodEffect());
            }
            else
            {
                _faialExpression.Wrong();
                StartCoroutine(BadEffect());
            }
            //リセット
            _choicesCount = 0;
            _loopCount += 1;
        }
        _duringAnimation = false;
    }
    public void ChoiceDisplay()
    {
        //選択肢を表示する
        _randomKeyArray=Randomize(4);
        if (_textW != null)
        {
            _textW.text = _choicesNeo[_loopCount,_choicesCount, _randomKeyArray[0]];
        }

        if (_textA != null)
        {
            _textA.text = _choicesNeo[_loopCount,_choicesCount, _randomKeyArray[1]];
        }

        if (_textS != null)
        {
            _textS.text = _choicesNeo[_loopCount, _choicesCount, _randomKeyArray[2]];
        }

        if (_textD != null)
        {
            _textD.text = _choicesNeo[_loopCount, _choicesCount, _randomKeyArray[3]];
        }
    }

    IEnumerator GoodEffect()
    {
        _goodEffect.SetActive(true);
        yield return new WaitForSeconds(7f);
        _goodEffect.SetActive(false);
    }

    IEnumerator BadEffect()
    {
        _badEffect.SetActive(true);
        yield return new WaitForSeconds(7f);
        _badEffect.SetActive(false);
    }
    public void ArrayInsert(int loop, int cLoop, int key, string[] choices)
    {
        int i = 0, j = 0, n = 0;
        _choicesNeo = new string[loop, cLoop, key];
        foreach (var choice in choices)
        {
            _choicesNeo[i, j, n] = choice;
            n++;
            if (n == key)
            {
                j++;
                n = 0;
                if (j == cLoop)
                {
                    i++;
                    j = 0;
                }
            }
        }
    }
    int[] Randomize(int n)
    {
        int[] array = new int[n];
        int[] keyArray =new int[n];
        for(int i = 0; i < n; i++)
        {
            keyArray[i] = i;
        }
        for (int i = 0; i < n; i++)
        {
            int random=Random.Range(0, n-i);
            array[i]=keyArray[random];
            keyArray[random] = keyArray[n-1-i];
        }
        return array;
    }
}