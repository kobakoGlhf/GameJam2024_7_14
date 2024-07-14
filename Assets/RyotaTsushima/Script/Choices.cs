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
    string[] _selectedChoices = new string[8];
    public int _choicesCount;
    public static int _score;
    public bool _gameStart;

    private void Start()
    {
        //関数のリセット
        for(int i=0; i<8; i++)
        {
            _selectedChoices[i] = null;
        }
        _choicesCount = 0;
        _goodEffect.SetActive(false);
        _badEffect.SetActive(false);
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
        StartCoroutine(ChoicesTimer());
    }
    private void Update()
    {
        if (_gameStart)
        {
            Debug.Log(_choicesCount);
            if (Input.GetKeyDown(KeyCode.W))
            {
                ChoiceW();
                Debug.Log("W");
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
            ChoiceDisplay();
        }
    }
    public void ChoiceW()　　//Wを検出したら起動
    {
        Debug.Log("Input.W");
        if (_selectedChoices[_choicesCount] == null) //選択肢を選んでなかったら
        {
            StopCoroutine(ChoicesTimer());
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4]; //選択肢を保存
            _choicesCount++; //カウントを増やす
        }
    }

    public void ChoiceA()　　//Aを検出したら起動
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 1];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
        }
    }

    public void ChoiceS()　　//Sを検出したら起動
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 2];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
        }
    }

    public void ChoicD()　　//Dを検出したら起動
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 3];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
        }
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

    public IEnumerator ChoicesTimer()   //選択のタイマー
    {
        float timer = 0;
        while (timer <= _limitedTime)
        {
            if (_selectedChoices[_choicesCount] == null)
            {
                yield return null;
            }
            else
            {
                yield break;
            }
            timer += Time.deltaTime;
        }
        int _random = Random.Range(0, 3);
        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + _random];
        _choicesCount++;
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