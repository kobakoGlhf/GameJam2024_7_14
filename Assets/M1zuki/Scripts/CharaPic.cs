using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaPic : MonoBehaviour
{
    public static int _charaNam = 0; //�I�������L�����̔ԍ�
    bool _isFirst = false;
    [SerializeField] GameObject _fadePanel;
    GameObject _bgmObj;
    Animator _bgmAnimator;
    [SerializeField] int _changeSceneWaitTime = 2;
    [SerializeField,Tooltip("�L����1�V�[��")] string _sceneName0;
    [SerializeField, Tooltip("�L����2�V�[��")] string _sceneName1;
    [SerializeField, Tooltip("�L����3�V�[��")] string _sceneName2;

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
                NextScene("SceneChange1");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _charaNam = 2;
                NextScene("SceneChange2");
            }
        }
    }
    void NextScene(string methodName)
    {
        _fadePanel.SetActive(true);
        _isFirst = true;
        _bgmAnimator.enabled = true;
        Invoke(methodName, _changeSceneWaitTime);
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
