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
    [SerializeField] GameObject _goodEffect;
    [SerializeField] GameObject _badEffect;
    [SerializeField] Slider _timeSlider;
    public string[] _selectedChoices = new string[8];
    public int _choicesCount;
    public static int _score;
    public bool _inGame;//���O�ύX�A�Q�[�������ǂ����̃t���O
    TalkText _talkText;
    Coroutine _coroutineTimer;//null�����悤

    private void Start()
    {
        _talkText = GetComponent<TalkText>();
        //_goodEffect.SetActive(false);
        //_badEffect.SetActive(false);
    }
    public void ChoiceDisplay()
    {
        //�I������\������
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
                    _coroutineTimer = null;//fix �R���[�`�����Z�b�g
                    _selectedChoices[_choicesCount] = _choices[_choicesCount * 4]; //�I������ۑ�
                    _choicesCount++; //�J�E���g�𑝂₷
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
    public void ChoiceW()�@�@//W�����o������N��
    {
        Debug.Log("Input.W");
        if (_selectedChoices[_choicesCount] == "") //�I������I��łȂ�������
        {
            StopCoroutine(ChoicesTimer());
            _coroutineTimer = null;//fix �R���[�`�����Z�b�g
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4]; //�I������ۑ�
            _choicesCount++; //�J�E���g�𑝂₷
            TextChange();
            Debug.Log(_choicesCount);
        }
        else Debug.Log(_selectedChoices[_choicesCount]);
    }

    public void ChoiceA()�@�@//A�����o������N��
    {
        if (_selectedChoices[_choicesCount] == "")
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 1];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
            TextChange();
        }
    }

    public void ChoiceS()�@�@//S�����o������N��
    {
        if (_selectedChoices[_choicesCount] == "")
        {
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 2];
            StopCoroutine(ChoicesTimer());
            _choicesCount++;
            TextChange();
        }
    }

    public void ChoicD()�@�@//D�����o������N��
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
        ChoiceDisplay(); //�\����ύX����
    }

    public void AddScore(int times, int correct1, int correct2, int correct3)   //�X�R�A���Z�@�����̃C���f�b�N�X����͂��Ă�������
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

    public IEnumerator ChoicesTimer()   //�I���̃^�C�}�[ //fix �����̏C��
    {
        float timer = 0;
        Debug.Log("�R���[�`��start");
        while (timer <= _limitedTime)
        {
            if (_selectedChoices[_choicesCount] == "")
            {
                yield return null;
            }
            else
            {
                //yield break;
                break;//�ύX
            }
            timer += Time.deltaTime;
        }
        int _random = Random.Range(0, 3);
        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + _random];
        _choicesCount++;
        TextChange();
    }

    public void Debuga()   //�f�o�b�O�p
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
//�R���[�`���̃��Z�b�g�ɂ͊֐���null�ɂ���K�v������܂��B