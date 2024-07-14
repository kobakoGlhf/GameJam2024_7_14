using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TalkText : MonoBehaviour
{
    int _index = 0;
    string[] _words;
    [SerializeField] string[] _talk;
    [SerializeField] Text _text;
    [SerializeField] GameObject _masseageBox;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Dialogue());
    }

    // Update is called once per frame
    void Update()
    {
        if (_index >= 3)
        {
            StartCoroutine(TextHide());
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _text.text = "";
            _index++;
            StartCoroutine(Dialogue());
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
    }

    IEnumerator TextHide()
    {
        yield return new WaitForSeconds(3f);
        _masseageBox.SetActive(false);
        _text.gameObject.SetActive(false);
    }
}
