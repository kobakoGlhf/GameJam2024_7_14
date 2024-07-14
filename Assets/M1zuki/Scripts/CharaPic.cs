using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaPic : MonoBehaviour
{
    public static int _charaNam = 0;
    bool _isFirst = false;
    [SerializeField] GameObject _fadePanel;
    GameObject _bgmObj;
    Animator _bgmAnimator;
    [SerializeField] int _changeSceneWaitTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        _charaNam = 0;
        _fadePanel.SetActive(false);
        _bgmObj = GameObject.Find("BGM");
        _bgmAnimator = _bgmObj.GetComponent<Animator>();
        _bgmAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterPic();
    }

    void CharacterPic()
    {
        if (!_isFirst)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _charaNam = 0;
                NextScene();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _charaNam = 1;
                NextScene();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _charaNam = 2;
                NextScene();
            }
        }
    }
    void NextScene()
    {
        _fadePanel.SetActive(true);
        _isFirst = true;
        _bgmAnimator.enabled = true;
        Invoke("SceneChange", _changeSceneWaitTime);
    }

    void SceneChange()
    {
        Destroy(_bgmObj);
        SceneManager.LoadScene("TestScene_Mizuki");
    }
}
