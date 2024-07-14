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
    string[] _selectedChoices = new string[8];
    int _choicesCount;
    
    public void ChoiceDisplay()
    {
        //選択肢を表示する &　タイマー起動
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
        StartCoroutine(ChoicesTimer());
    }

    public void ChoiceW()　　//Wを検出したら起動
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
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

    IEnumerator ChoicesTimer()   //選択のタイマー
    {
        yield return new WaitForSeconds(_limitedTime);
        if (_selectedChoices[_choicesCount] == null)
        {
            int _random = Random.Range(0, 3);
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + _random];
        }
        _choicesCount++;
    }

    public void Debuga(int num)   //デバッグ用
    {
        Debug.Log(_selectedChoices[num]);
    }
}