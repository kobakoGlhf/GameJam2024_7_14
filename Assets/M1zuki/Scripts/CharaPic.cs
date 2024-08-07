using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaPic : MonoBehaviour
{
    public static int _charaNam = 0; //選択したキャラの番号
    bool _isFirst = false;
    [SerializeField] GameObject _fadePanel;
    GameObject _bgmObj;
    Animator _bgmAnimator;
    [SerializeField] int _changeSceneWaitTime = 2;
    [SerializeField] SEObj _seObj;
    [SerializeField,Tooltip("キャラ1シーン")] string _sceneName0;
    [SerializeField, Tooltip("キャラ2シーン")] string _sceneName1;
    [SerializeField, Tooltip("キャラ3シーン")] string _sceneName2;
    [SerializeField, Tooltip("SEのクリップ")] AudioClip _clip;

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
                NextScene("SceneChange0");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                _charaNam = 1;
                NextScene("SceneChange0");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _charaNam = 2;
                NextScene("SceneChange0");
            }
        }
    }
    void NextScene(string methodName)
    {
        _seObj.PlaySe(_clip);
        _fadePanel.SetActive(true);
        _bgmAnimator.enabled = true;
        Invoke(methodName, _changeSceneWaitTime);
        _isFirst = true;
    }

    void SceneChange0()
    {
        Destroy(_bgmObj);
        SceneManager.LoadScene(_sceneName0);
    }
    void SceneChange1()
    {
        Destroy(_bgmObj);
        SceneManager.LoadScene(_sceneName1);
    }
    void SceneChange2()
    {
        Destroy(_bgmObj);
        SceneManager.LoadScene(_sceneName2);
    }
}
