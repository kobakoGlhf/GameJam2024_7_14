using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaPic : MonoBehaviour
{
    public static int _charaNam = 0;
    bool _isFirst = false;
    [SerializeField] GameObject _fadePanel;
    [SerializeField] Animator _bgmObj;
    [SerializeField] int _changeSceneWaitTime = 2;

    // Start is called before the first frame update
    void Start()
    {
        _charaNam = 0;
        _fadePanel.SetActive(false);
        _bgmObj.enabled = false;
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
        _bgmObj.enabled = true;
        Invoke("SceneChange", _changeSceneWaitTime);
    }

    void SceneChange()
    {
        SceneManager.LoadScene("TestScene_Mizuki");
    }
}
