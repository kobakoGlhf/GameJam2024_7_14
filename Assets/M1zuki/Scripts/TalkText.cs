using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{
    bool _isFirst;
    bool _isFirst2;
    bool _isFirst3;
    public int _index = 0;
    string[] _words;
    Choices _choices;
    [SerializeField] string[] _talk;
    public Text _text;
    [SerializeField] GameObject _masseageBox;
    [SerializeField] SEObj _seObj;
    [SerializeField,Tooltip("�e�L�X�g�̃J�^�J�^��")] AudioClip _textSeClip;
    Coroutine _dialogue;

    // Start is called before the first frame update
    void Start()
    {
        _seObj.PlaySe(_textSeClip);
        _dialogue = StartCoroutine("Dialogue");
        _choices = GetComponent<Choices>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_index == 3 && !_isFirst)
        {
            _text.text = "�i�Ȃ�ăR�����g���悤���ȁH�H�j";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TextHidden();
                _isFirst = true;
                enabled = false;
            }
            return;
        }
        else if (_index == 5 && !_isFirst2)
        {
            _text.text = "�i�Ȃ�ăR�����g���悤���ȁH�H�j";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TextHidden();
                _isFirst2 = true;
                _choices.enabled = true;
                enabled = false;
            }
            return;
        }
        else if (_index == 7 && !_isFirst3)
        {
            _text.text = "�i�Ȃ�ăR�����g���悤���ȁH�H�j";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TextHidden();
                _isFirst3 = true;
                _choices.enabled = true;
                enabled = false;
            }
            return;
        }
        else if (_index == 9)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("ResultScene");
            }
        }

        //�X�y�[�X����������i��
        if (Input.GetKeyDown(KeyCode.Space) && _dialogue == null&&_index!=9)
        {
            _seObj.PlaySe(_textSeClip); //SE�炷�Ƃ���
            _text.text = "";
            _index++;
            _dialogue = StartCoroutine(Dialogue());
        }
        //�f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.O))
        {
            TextHidden();
        }
    }
    void TextHidden()
    {
        _masseageBox.SetActive(false);
        _text.gameObject.SetActive(false);
        _choices.ChoiceDisplay();
        _choices.TimerStart();
        _choices._inGame = true;
        _choices._question.SetActive(true);
    }

    public void TextActive()
    {
        _masseageBox.SetActive(true);
        _text.gameObject.SetActive(true);
        _choices._inGame = false;
    }

    IEnumerator Dialogue() //1�������\������
    {
        _words = _talk[_index].Split(' ');

        foreach (string word in _words)
        {
            _text.text = _text.text + word;
            yield return new WaitForSeconds(0.1f);
        }
        _dialogue = null;
    }
}
