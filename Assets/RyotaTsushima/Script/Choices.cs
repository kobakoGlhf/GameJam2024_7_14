using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Choices : MonoBehaviour
{
    [SerializeField,Tooltip("��񂠂���̐�������")] float _limitedTime;
    [SerializeField,Tooltip("�I����")] string[] _choices;
    [SerializeField] Text _textW;
    [SerializeField] Text _textA;
    [SerializeField] Text _textS;
    [SerializeField] Text _textD;
    string[] _selectedChoices = new string[8];
    int _choicesCount;
    
    public void ChoiceDisplay()
    {
        //�I������\������ &�@�^�C�}�[�N��
        if(_textW != null)
        {
            _textW.text = _choices[_choicesCount * 4];
        }
        else
        {
            Debug.Log("W�ɑΉ�����I����������܂���");
        }

        if (_textA != null)
        {
            _textA.text = _choices[_choicesCount * 4 + 1];
        }
        else
        {
            Debug.Log("A�ɑΉ�����I����������܂���");
        }

        if (_textS != null)
        {
            _textS.text = _choices[_choicesCount * 4 + 2];
        }
        else
        {
            Debug.Log("S�ɑΉ�����I����������܂���");
        }

        if (_textD != null)
        {
            _textD.text = _choices[_choicesCount * 4 + 3];
        }
        else
        {
            Debug.Log("D�ɑΉ�����I����������܂���");
        }
        StartCoroutine(ChoicesTimer());
    }

    public void ChoiceW()�@�@//W�����o������N��
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
        }
    }

    public void ChoiceA()�@�@//A�����o������N��
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 1];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
        }
    }

    public void ChoiceS()�@�@//S�����o������N��
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 2];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
        }
    }

    public void ChoicD()�@�@//D�����o������N��
    {
        if (_selectedChoices[_choicesCount] == null)
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 3];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
        }
    }

    IEnumerator ChoicesTimer()   //�I���̃^�C�}�[
    {
        yield return new WaitForSeconds(_limitedTime);
        if (_selectedChoices[_choicesCount] == null)
        {
            int _random = Random.Range(0, 3);
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + _random];
        }
        _choicesCount++;
    }

    public void Debuga(int num)   //�f�o�b�O�p
    {
        Debug.Log(_selectedChoices[num]);
    }
}