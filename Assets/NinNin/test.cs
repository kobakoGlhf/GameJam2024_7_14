using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] int _count = 0;

    [SerializeField] GameObject _obj;

    DisplayCount _displayCount;

    void Start()
    {
        _displayCount = _obj.GetComponent<DisplayCount>();
    }
    void Update()
    {
        _displayCount._subscribers = _count;
    }
}
