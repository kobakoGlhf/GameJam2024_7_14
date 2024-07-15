using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] int _count = 0;

    [SerializeField] UiSubscribers _displayCount;
    [SerializeField] UiTimeLimit _timeLimit;
    [SerializeField] Canvas _canvas;


    void Update()
    {
        _displayCount._count = _count;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;

            _timeLimit.Display(3.0f, mousePos, _canvas);
        }
    }
}
