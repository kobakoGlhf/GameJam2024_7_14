using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    [SerializeField,Tooltip("一回あたりの制限時間")] float _limitedTime;
    [SerializeField,Tooltip("選択肢")] string[] _choices;
    [SerializeField] Text _textW;
    [SerializeField] Text _textA;
    [SerializeField] Text _textS;
    [SerializeField] Text _textD;
    [SerializeField] GameObject _goodEffect;
    [SerializeField] GameObject _badEffect;
    [SerializeField] Slider _timeSlider;
    public string[] _selectedChoices = new string[8];
    public int _choicesCount;
    public static int _score;
    public bool _inGame;//名前変更、ゲーム中かどうかのフラグ
    TalkText _talkText;
    Coroutine _coroutineTimer;//null入れるよう

    private void Start()
    {
        _talkText = GetComponent<TalkText>();
        //_goodEffect.SetActive(false);
        //_badEffect.SetActive(false);
    }
    public void ChoiceDisplay()
    {
        //選択肢を表示する
        if(_textW != null)
        {
            _textW.text = _choices[_choicesCount * 4];
        }
        else
        {
            Debug.Log("Wに対応する選択肢がありません");
        }

        if (_textA != null)
        {
            _textA.text = _choices[_choicesCount * 4 + 1];
        }
        else
        {
            Debug.Log("Aに対応する選択肢がありません");
        }

        if (_textS != null)
        {
            _textS.text = _choices[_choicesCount * 4 + 2];
        }
        else
        {
            Debug.Log("Sに対応する選択肢がありません");
        }

        if (_textD != null)
        {
            _textD.text = _choices[_choicesCount * 4 + 3];
        }
        else
        {
            Debug.Log("Dに対応する選択肢がありません");
        }
    }

    public void TimerStart()
    {
        _coroutineTimer=StartCoroutine(ChoicesTimer());//fix
    }
    private void Update()
    {
        if (_inGame)
        {
            //Debug.Log(_choicesCount);
            if (Input.GetKeyDown(KeyCode.W))
            {
                if ((_choicesCount + 1) % 3 != 0)
                {
                    ChoiceW();
                    Debug.Log("W");
                    Debug.Log((_choicesCount / 4 + 1) % 3);
                }
                else if (_selectedChoices[_choicesCount]=="")
                {
                    StopCoroutine(ChoicesTimer());
                    _coroutineTimer = null;//fix コルーチンリセット
                    _selectedChoices[_choicesCount] = _choices[_choicesCount * 4]; //選択肢を保存
                    _choicesCount++; //カウントを増やす
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                ChoiceA();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                ChoiceS();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                ChoicD();
            }
        }
    }
    public void ChoiceW()　　//Wを検出したら起動
    {
        Debug.Log("Input.W");
        if (_selectedChoices[_choicesCount] == "") //選択肢を選んでなかったら
        {
            StopCoroutine(ChoicesTimer());
            _coroutineTimer = null;//fix コルーチンリセット
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4]; //選択肢を保存
            _choicesCount++; //カウントを増やす
            TextChange();
            Debug.Log(_choicesCount);
        }
        else Debug.Log(_selectedChoices[_choicesCount]);
    }

    public void ChoiceA()　　//Aを検出したら起動
    {
        if (_selectedChoices[_choicesCount] == "")
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 1];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
            TextChange();
        }
    }

    public void ChoiceS()　　//Sを検出したら起動
    {
        if (_selectedChoices[_choicesCount] == "")
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 2];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
            TextChange();
        }
    }

    public void ChoicD()　　//Dを検出したら起動
    {
        if (_selectedChoices[_choicesCount] == "")
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 3];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
            TextChange();
        }
    }

    void TextChange()
    {
        if (_choicesCount == 3)
        {
            _talkText.enabled = true;
            _talkText._text.text = "";
            _talkText.TextActive();
            enabled = false;
        }
        else if (_choicesCount == 6)
        {
            _talkText.enabled = true;
            _talkText._text.text = "";
            _talkText.TextActive();
            enabled = false;
        }
        else if (_choicesCount == 8)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                _talkText.enabled = true;
                _talkText._text.text = "";
                _talkText.TextActive();
                enabled = false;
            }
        }
        ChoiceDisplay(); //表示を変更する
    }

    public void AddScore(int times, int correct1, int correct2, int correct3)   //スコア加算　正解のインデックスを入力してください
    {                                                        
        if (_selectedChoices[times * 3] == _choices[correct1]
            && _selectedChoices[times * 3 +1] == _choices[correct2] 
            && _selectedChoices[times * 3 + 2] == _choices[correct3])
        {
            _score++;
            StartCoroutine(GoodEffect());
        }
        else
        {
            StartCoroutine(BadEffect());
        }
    }

    public IEnumerator ChoicesTimer()   //選択のタイマー //fix 挙動の修正
    {
        float timer = 0;
        Debug.Log("コルーチンstart");
        while (timer <= _limitedTime)
        {
            if (_selectedChoices[_choicesCount] == "")
            {
                yield return null;
            }
            else
            {
                //yield break;
                break;//変更
            }
            timer += Time.deltaTime;
        }
        int _random = Random.Range(0, 3);
        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + _random];
        _choicesCount++;
        TextChange();
    }

    public void Debuga()   //デバッグ用
    {
        StartCoroutine(GoodEffect());
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
}
//コルーチンのリセットには関数をnullにする必要があります。