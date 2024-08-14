using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    [SerializeField] GameObject _titlelogo;
    [SerializeField] Text[] _text;
    [SerializeField] GameObject _bgmObj;
    SwitchScenes _scenes;
    [SerializeField] int _index;
    [SerializeField] GameObject _creditImage;
    private bool _imageDisplayed = false;
    [SerializeField] SEObj _seObj;
    [SerializeField] AudioClip _cursor;
    [SerializeField] AudioClip _determination;
    bool _operationModeKay = true;


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
        
        if (!_imageDisplayed )//&& _operationModeKay)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _seObj.PlaySe(_cursor);
                _index = 0;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _seObj.PlaySe(_cursor);
                _index = 1;
            }
            _operationModeKay = Input.GetAxisRaw("Mouse X") == 0 || Input.GetAxisRaw("Mouse Y") == 0;
        }
        TextColorCange(_index);



        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_index == 0)
            {
                _seObj.PlaySe(_determination);
                _scenes.ChangeScene();
            }
            else if (_index == 1)
            {
                _seObj.PlaySe(_determination);
                ToggleImageDisplay();
            }
        }
    }

    public void ToggleImageDisplay()
    {
        foreach (var text in _text)
        {
            text.gameObject.SetActive(_imageDisplayed);
        }
        _titlelogo.SetActive(_imageDisplayed);
        _creditImage.SetActive(!_imageDisplayed);
        _imageDisplayed = !_imageDisplayed;
    }
    void TextColorCange(int textArraySum)
    {
        foreach (var text in _text)
        {
            if (text == _text[textArraySum])
            {
                text.color = Color.red;
            }
            else text.color = Color.white;
        }
    }
    void TextColorCange()//すべてのテキストを白くする用
    {
        foreach (var text in _text)
        {
            text.color = Color.white;
        }
    }
    public void indexchange(int num)
    {
        _index = num;
    }
}
