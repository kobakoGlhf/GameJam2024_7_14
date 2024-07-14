using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    int _index;
    [SerializeField] Text[] _text;
    [SerializeField] GameObject _bgmObj;
    // Start is called before the first frame update
    void Start()
    {
        _text[0].color = Color.red;
        _text[1].color = Color.white;
        DontDestroyOnLoad(_bgmObj);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _text[0].color = Color.red;
            _text[1].color = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _text[1].color = Color.red;
            _text[0].color = Color.white;
        }
    }
}
