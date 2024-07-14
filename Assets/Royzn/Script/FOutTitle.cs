using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FOutTitle : MonoBehaviour
{
    public GameObject targetObject;

    public void ShowObject()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(true);
        }
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
