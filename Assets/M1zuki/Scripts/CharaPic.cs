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
    [SerializeField, Tooltip("ゲームシーン")] string _sceneName0;
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
        if (!_isFirst)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                CharacterPic(0);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                CharacterPic(1);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                CharacterPic(2);
            }
        }
    }

    public void CharacterPic(int num)
    {
        _charaNam = num;
        NextScene("SceneChange0");
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
}
