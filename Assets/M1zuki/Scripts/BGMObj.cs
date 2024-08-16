using UnityEngine;

public class BGMObj : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        BGMObj[] bgmObj = FindObjectsOfType<BGMObj>();
        foreach (BGMObj obj in bgmObj)
        {
            if (obj != gameObject.GetComponent<BGMObj>())
            {
                Destroy(gameObject);
            }
        }
    }
    void Start()
    {
        _animator = GetComponent<Animator>();
        if(_animator != null) _animator.enabled = false;
    }
    public void AnimationPlay()
    {
        if (_animator != null) _animator.enabled = true;
    }
}
