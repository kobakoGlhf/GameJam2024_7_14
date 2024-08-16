using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimation : MonoBehaviour
{
    [SerializeField]BGMObj _bgmObj;
    public void OnAnimationEnd()
    {
        if(_bgmObj!=null) Destroy(_bgmObj.gameObject);
        SceneManager.LoadScene("Title");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
