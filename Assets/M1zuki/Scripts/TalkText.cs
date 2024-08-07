using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Choices _choices;
    [SerializeField] public string[] _talk;
    public string[][] _talkText = new string[4][];
    public Text _text;
    [SerializeField] GameObject _masseageBox;
    [SerializeField] SEObj _seObj;
    [SerializeField, Tooltip("テキストのカタカタ音")] AudioClip _textSeClip;
    Coroutine _dialogue;
    int _talkPhase;
    // Start is called before the first frame update
    void Start()
    {
        _seObj.PlaySe(_textSeClip);
        _dialogue = StartCoroutine(Dialogue());
        _index++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_index == _talkText[_talkPhase].Length && _talkPhase != _talkText.Length - 1)
            {
                _index = 0;
                TextHidden();
                _choices.enabled = true;
                enabled = false;
                _talkPhase++;
                return;
            }
            else if (_talkPhase == _talkText.Length - 1 && _index == _talkText[_talkPhase].Length)
            {
                SceneManager.LoadScene("ResultScene");
            }

            //スペースを押したら進む
            else if (_dialogue == null)
            {
                _seObj.PlaySe(_textSeClip); //SE鳴らすところ
                _text.text = "";
                _dialogue = StartCoroutine(Dialogue());
                _index++;
            }
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

    IEnumerator Dialogue() //1文字ずつ表示する
    {
        _words = _talkText[_talkPhase][_index].Split(' ');

        foreach (string word in _words)
        {
            _text.text = _text.text + word;
            yield return new WaitForSeconds(0.1f);
        }
        _dialogue = null;
    }
    public void TalkTextInArray(string[] text)
    {
        List<string> strings = new List<string>();
        int i = 0;
        foreach (string word in text)
        {
            strings.Add(word);
            if (word == "（なんてコメントしようかな？？）" || text[text.Length - 1] == word)
            {
                _talkText[i] = new string[strings.Count];
                for (int j = 0; j < strings.Count; j++)
                {
                    _talkText[i][j] = strings[j];
                }
                i++;
                strings.Clear();
                if (i > _talkText.Length)
                {
                    break;
                }
            }
        }
    }
}
