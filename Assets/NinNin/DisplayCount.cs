using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCount : MonoBehaviour
{
    int _goalCount;
    int _currentCount = 0;

    [SerializeField] Text _text;
    /// <summary>���b���Ƃ�Text���X�V���邩</summary>
    [SerializeField] float _interval = 0.01f;
    /// <summary>1���[�v���Ƃɑ��������</summary>
    [SerializeField] int _increase = 1;

    /// <summary>�O�����炱�̕ϐ��𒼐ڂ������āA�o�^�Ґ���\������B</summary>
    public int _subscribers
    {
        get => _goalCount;
        set
        {
            if (value < 0)
            {
                Debug.Log("�}�C�i�X�̒l�͓��͂ł��܂���");
                return;
            }
            _goalCount = value;
        }
    }


    private void Start()
    {
        StartCoroutine(counter()); //�R���[�`�����J�n
    }
    private void Update()
    {
        //currentText����e�L�X�g�ɕ\������
        _text.text = _currentCount.ToString() + " �l";
    }

    IEnumerator counter()
    {
        while (true)
        {
            // �S�[���Ƃ̗����ɂ���āA_increase�𑝌�������
            {
                int difference;
                difference = Mathf.Abs(_goalCount - _currentCount); //�������

                _increase = difference / 10;

                if (_increase <= 0) //�Œ�l��1
                {
                    _increase = 1;
                }
            }

            // currentCount���AgoalCount����A���񂾂�Ƒ��₷
            // ���₷
            if (_currentCount < _goalCount)
            {
                int tmp = _currentCount + _increase;//�������Ōv�Z

                // �����A�ߏ�ɑ����邱�Ƃ��Ȃ��Ȃ�..
                if (tmp < _goalCount)
                {
                    _currentCount += _increase;
                }
                //�����A�ߏ�ɑ���������Ȃ�..
                else
                {
                    _currentCount = _goalCount;
                }
            }
            // ���炷
            else if(_currentCount > _goalCount)
            {
                int tmp = _currentCount - _increase;//�������Ōv�Z

                // �����A�ߏ�Ɍ��炷���Ƃ��Ȃ��Ȃ�..
                if (tmp > _goalCount)
                {
                    _currentCount -= _increase;
                }
                //�����A�ߏ�Ɍ��炵������Ȃ�..
                else
                {
                    _currentCount = _goalCount;
                }
            }

            // �C���^�[�o�����A�҂�
            yield return new WaitForSeconds(_interval);
        }
    }

}
