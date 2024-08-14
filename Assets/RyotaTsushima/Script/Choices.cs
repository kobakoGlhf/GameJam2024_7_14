using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UiTimeLimit))]
public class Choices : MonoBehaviour
{
    [SerializeField, Tooltip("��񂠂���̐�������")] float _limitedTime;
    [SerializeField, Tooltip("�I����")] public string[] _choices;
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
    public bool _inGame;//���O�ύX�A�Q�[�������ǂ����̃t���O
    [SerializeField] public GameObject _question;
    TalkText _talkText;
    [SerializeField] GameObject _timerPrefab;
    UiTimeLimit _timeLimit;
    [SerializeField] RectTransform _timerRectTransform;
    [SerializeField] ChatManager _chatManager;
    [SerializeField] ChoicesMoveManager _choiceMoveManager;
    FacialExpression _faialExpression;
    int _currentScore;
    public string[,,] _choicesNeo;//choices���g���₷���i�[���邽�߂̔z��B      ������loop�񐔁A�I���񐔁A�I������
    int _loopCount;//���[�v�񐔁B
    public int _choicesCount;//1���[�v���Ƃɂ̑I���ł����
    Coroutine _coroutineTimer;//null�����悤
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
    public void ChoiceButton(int sum)// EventTrigger�ŌĂяo����悤�ɂ߂�ǂ��������Ƃ��Ă܂��B
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
            _coroutineTimer = null;//fix �R���[�`�����Z�b�g
            _selectedChoices[_choicesCount] = _choicesNeo[_loopCount, _choicesCount, sum]; //�I������ۑ�
            _choicesCount++; //�J�E���g�𑝂₷
            StartCoroutine(ChoiceEffect(_choicesCount == 3)); 
            _audioSource.PlayOneShot(_audioClip);
        }
    }
    public IEnumerator ChoicesTimer()   //�I���̃^�C�}�[ //fix �����̏C��
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
            ChoiceDisplay(); //�\����ύX����
            yield return new WaitForSeconds(_choiceMoveManager._animetionTimer / 2);
            _timeLimit.DestroyTimer();
            TimerStart();
        }
        else //loop�I�����̏���
        {
            _timeLimit.DestroyTimer();
            _talkText.enabled = true;
            _talkText._text.text = "";
            _talkText.TextActive();
            _question.gameObject.SetActive(false);
            enabled = false;
            _currentScore = Score._score;
            _score.AddScore();
            //�p�[�e�B�N���V�X�e���ɂ��G�t�F�N�g�Đ�����
            _choiceEffect.AnimationPlay(_selectedChoices[0] + _selectedChoices[1] + _selectedChoices[2]);//�\�����镶���������
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
            //���Z�b�g
            _choicesCount = 0;
            _loopCount += 1;
        }
        _duringAnimation = false;
    }
    public void ChoiceDisplay()
    {
        //�I������\������
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