using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [HideInInspector] public string[] _selectedChoices = new string[9];
    public int _choicesCount;
    public bool _inGame;//���O�ύX�A�Q�[�������ǂ����̃t���O
    [SerializeField] public GameObject _question;
    TalkText _talkText;
    FacialExpression _faialExpression;
    int _currentScore;

    Coroutine _coroutineTimer;//null�����悤

    private void Start()
    {
        _score=GameObject.FindObjectOfType<Score>();
        _talkText = GetComponent<TalkText>();
        _question.gameObject.SetActive(false);
        //_goodEffect.SetActive(false);
        //_badEffect.SetActive(false);

        _faialExpression = GetComponent<FacialExpression>();
    }
    public void ChoiceDisplay()
    {
        //�I������\������
        if (_textW != null)
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
        _coroutineTimer = StartCoroutine(ChoicesTimer());//fix
    }
    private void Update()
    {
        if (_inGame)
        {
            //Debug.Log(_choicesCount);
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
    public void ChoiceButton(int sum)
    {
        if (_selectedChoices[_choicesCount] == "") //�I������I��łȂ�������
        {
            StopCoroutine(ChoicesTimer());
            _coroutineTimer = null;//fix �R���[�`�����Z�b�g
            _selectedChoices[_choicesCount] = _choices[_choicesCount * 4+sum]; //�I������ۑ�
            _choicesCount++; //�J�E���g�𑝂₷
            TextChange();
        }
    }
    //public void ChoiceW()�@�@//W�����o������N��
    //{
    //    Debug.Log("Input.W");
    //    if (_selectedChoices[_choicesCount] == "") //�I������I��łȂ�������
    //    {
    //        StopCoroutine(ChoicesTimer());
    //        _coroutineTimer = null;//fix �R���[�`�����Z�b�g
    //        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4]; //�I������ۑ�
    //        _choicesCount++; //�J�E���g�𑝂₷
    //        TextChange();
    //    }
    //}

    //public void ChoiceA()�@�@//A�����o������N��
    //{
    //    if (_selectedChoices[_choicesCount] == "")
    //    {
    //        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 1];
    //        StopCoroutine(ChoicesTimer());
    //        _choicesCount++;
    //        TextChange();
    //    }
    //}

    //public void ChoiceS()�@�@//S�����o������N��
    //{
    //    if (_selectedChoices[_choicesCount] == "")
    //    {
    //        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 2];
    //        StopCoroutine(ChoicesTimer());
    //        _choicesCount++;
    //        TextChange();
    //    }
    //}

    //public void ChoicD()�@�@//D�����o������N��
    //{
    //    if (_selectedChoices[_choicesCount] == "")
    //    {
    //        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + 3];
    //        StopCoroutine(ChoicesTimer());
    //        _choicesCount++;
    //        TextChange();
    //    }
    //}

    void TextChange()
    {
        if (_choicesCount == 3)
        {
            _talkText.enabled = true;
            _talkText._text.text = "";
            _talkText.TextActive();
            _question.gameObject.SetActive(false);
            enabled = false;
            _currentScore = _score._score;
            _score.AddScore(0, 0, 4, 8);
            _score.AddScore(0, 1, 5, 9);
            _score.AddScore(0, 2, 6, 10);
            _score.AddScore(0, 3, 7, 11);
            _choiceEffect.AnimationPlay(_selectedChoices[0*3]+ _selectedChoices[0 * 3+1]+ _selectedChoices[0 * 3+2]);
            //Debug.Log("aaa");
            if (_currentScore != _score._score)
            {
                _faialExpression.Correct();
            }
            else
            {
                _faialExpression.Wrong();
            }
        }
        else if (_choicesCount == 6)
        {
            _talkText.enabled = true;
            _talkText._text.text = "";
            _talkText.TextActive();
            _question.gameObject.SetActive(false);
            enabled = false;
            _currentScore = _score._score;
            _score.AddScore(1, 12, 16, 20);
            _score.AddScore(1, 13, 17, 21);
            _score.AddScore(1, 14, 18, 22);
            _score.AddScore(1, 15, 19, 23);
            _choiceEffect.AnimationPlay(_selectedChoices[1 * 3] + _selectedChoices[1 * 3 + 1] + _selectedChoices[1 * 3 + 2]);
            if (_currentScore != _score._score)
            {
                _faialExpression.Correct();
            }
            else
            {
                _faialExpression.Wrong();
            }
        }
        else if (_choicesCount == 9)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                _talkText.enabled = true;
                _talkText._text.text = "";
                _talkText.TextActive();
                _question.gameObject.SetActive(false);
                enabled = false;
                _currentScore = _score._score;
                _score.AddScore(2, 24, 28, 32);
                _score.AddScore(2, 25, 29, 33);
                _score.AddScore(2, 26, 30, 34);
                _score.AddScore(2, 27, 31, 35);
                _choiceEffect.AnimationPlay(_selectedChoices[1 * 3] + _selectedChoices[1 * 3 + 1] + _selectedChoices[1 * 3 + 2]);
                Debug.Log("�Q�[�������I��");
                if (_currentScore != _score._score)
                {
                    _faialExpression.Correct();
                }
                else
                {
                    _faialExpression.Wrong();
                }
            }
        }
        else
        {
            _coroutineTimer = StartCoroutine(ChoicesTimer());
            ChoiceDisplay(); //�\����ύX����
            Debug.Log(_choicesCount);
        }
    }


    public IEnumerator ChoicesTimer()   //�I���̃^�C�}�[ //fix �����̏C��
    {
        Debug.Log("�R���[�`��start");
        float timer = 0;
        int count = _choicesCount;
        while (timer <= _limitedTime)
        {
            if (_selectedChoices[count] == "")
            {
                yield return null;
            }
            else
            {
                Debug.Log("�R���[�`���I��");
                yield break;
            }
            timer += Time.deltaTime;
        }
        int _random = Random.Range(0, 3);
        _selectedChoices[_choicesCount] = _choices[_choicesCount * 4 + _random];
        _choicesCount++;
        TextChange();
        Debug.Log("�R���[�`������");
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