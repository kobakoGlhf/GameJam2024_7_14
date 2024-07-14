using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{
    bool _isFirst;
    bool _isFirst2;
    bool _isFirst3;
    bool _isFirst4;
    int _index = 0;
    string[] _words;
    Choices _choices;
    [SerializeField] string[] _talk;
    [SerializeField] Text _text;
    [SerializeField] GameObject _masseageBox;
    Coroutine _dialogue;

    // Start is called before the first frame update
    void Start()
    {
        _dialogue = StartCoroutine(Dialogue());
        _choices = GetComponent<Choices>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_index == 3 && !_isFirst)
        {
            StartCoroutine(TextHide());
            _isFirst = true;
        }
        if (_index == 5 && !_isFirst)
        {
            StartCoroutine(TextHide());
            _isFirst = true;
        }
        if (_index == 7 && !_isFirst)
        {
            StartCoroutine(TextHide());
            _isFirst = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _dialogue == null)
        {
            _text.text = "";
            _index++;
            _dialogue = StartCoroutine(Dialogue());
        }

        if(_choices._choicesCount == 3 && _dialogue == null)
        {
            if (!_isFirst2)
            {
                SecondRound();
            }
        }

        if (_choices._choicesCount == 6 && _dialogue == null)
        {
            if (!_isFirst3)
            {
                ThirdRound();
            }
        }

        if (_choices._choicesCount == 9 && _dialogue == null)
        {
            if (!_isFirst4)
            {
                Ending();
            }
        }
    }

    IEnumerator Dialogue()
    {
        _words = _talk[_index].Split(' ');

        foreach (string word in _words)
        {
            _text.text = _text.text + word;
            yield return new WaitForSeconds(0.1f);
        }
        _dialogue = null;
    }

    IEnumerator TextHide()
    {
        yield return new WaitForSeconds(2f);
        _masseageBox.SetActive(false);
        _text.gameObject.SetActive(false);
        _choices.ChoiceDisplay();
        _choices.TimerStart();
        _choices._gameStart = true;
        yield break;
    }

    void SecondRound()
    {
        _isFirst = false;
        _choices._gameStart = false;
        _masseageBox.SetActive(true);
        _text.gameObject.SetActive(true);
        _text.text = "";
        _index = 4;
        _dialogue = StartCoroutine(Dialogue());
        _isFirst2 = true;
    }

    void ThirdRound()
    {
        _isFirst = false;
        _choices._gameStart = false;
        _masseageBox.SetActive(true);
        _text.gameObject.SetActive(true);
        _text.text = "";
        _index = 6;
        _dialogue = StartCoroutine(Dialogue());
        _isFirst3 = true;
    }

    void Ending()
    {
        _isFirst = false;
        _choices._gameStart = false;
        _masseageBox.SetActive(true);
        _text.gameObject.SetActive(true);
        _text.text = "";
        _index = 8;
        _dialogue = StartCoroutine(Dialogue());
        _isFirst4 = true;
    }
}
