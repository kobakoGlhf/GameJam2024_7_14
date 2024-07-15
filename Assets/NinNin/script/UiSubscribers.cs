using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSubscribers : MonoBehaviour
{
    [SerializeField] Text _text;

    [SerializeField] int _goalCount;
    [SerializeField] int _currentCount = 0;

    [Tooltip("���b���Ƃ�Text���X�V���邩")]
    [SerializeField] float _interval = 0.01f;
    [Tooltip("1���[�v���Ƃɑ�������ʁB")]
    [SerializeField] int _increase = 1;
    [Tooltip("�S�[���l�ƌ��ݒl�̍��̑傫���ɂ���āAincrease�𑝌�����B\n�܂�A�����傫���قǑf������������B\nfalse�ɂ����ꍇ�ACurrentCount�̑����͏�Ɉ��ɂȂ�B")]
    [SerializeField] bool _SmoothIncrease = true;
    [Tooltip("���l�̉������Aincrease�ɐݒ肷�邩�B")]
    [SerializeField] float _SmoothIncreasePercent = 0.05f;

    // �v���p�e�B
    /// <summary>�O�����炱�̕ϐ��𒼐ڂ������āA�o�^�Ґ���\������B</summary>
    public int _count
    {
        get => _goalCount;
        set
        {
            if (value < 0)
            {
                Debug.LogWarning("�}�C�i�X�̒l�͓��͂ł��܂���");
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
        if (_text != null)
        {
            //currentText����e�L�X�g�ɕ\������
            _text.text ="�o�^�Ґ� " + _currentCount.ToString() + " �l";
        }
        else
        {
            Debug.LogWarning($"'{_text}' is null.", this.gameObject);
        }
    }

    IEnumerator counter()
    {
        while (true)
        {
            // �S�[���ƍ��l�ɂ���āA_increase�𑝌�������
            if (_SmoothIncrease == true)
            {
                float difference;
                difference = Mathf.Abs(_goalCount - _currentCount); //�������

                _increase = (int)(difference * _SmoothIncreasePercent);

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
