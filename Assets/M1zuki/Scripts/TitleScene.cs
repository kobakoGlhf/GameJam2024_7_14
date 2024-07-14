using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] Text[] _text;
    [SerializeField] GameObject _bgmObj;
    SwitchScenes _scenes;
    int _index;
    // Start is called before the first frame update
    void Start()
    {
        _scenes = GetComponent<SwitchScenes>();
        _text[0].color = Color.red;
        _text[1].color = Color.white;
        _index = 0;
        DontDestroyOnLoad(_bgmObj);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _text[0].color = Color.red;
            _text[1].color = Color.white;
            _index = 0;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _text[1].color = Color.red;
            _text[0].color = Color.white;
            _index = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_index == 0)
            {
                _scenes.ChangeScene();
            }
        }
    }
}
