using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBoxMove : MonoBehaviour
{
    [SerializeField] float _moveTime;
    [SerializeField] float _moveX;
    [SerializeField] float _moveY;
    float _x;
    float _y;
    float _dX;
    float _dY;
    Transform _tf;
    // Start is called before the first frame update
    void Start()
    {
        _tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move()
    {
        StartCoroutine(Moves());
    }

    IEnumerator Moves()
    {
        _x = gameObject.transform.position.x;
        _y = gameObject.transform.position.y;
        _dX = _moveX - _x;
        _dY = _moveY - _y;
        bool _moveFinished = false;
        //while (false)
        //{
        //    _x += _dX * Time.deltaTime;
        //    _y += _dY * Time.deltaTime;
        //    _tf.x = _x;

        //    yield return null;
        //    _x = gameObject.transform.position.x;
        //    _y = gameObject.transform.position.y;
        //    if (_x > 0 )
        //    {
        //        if (_x >= _moveX)
        //        {
        //            _moveFinished = true;
        //        }
        //    }
        //    else
        //    {
        //        if(_x<=_moveX)
        //        {
        //            _moveFinished = true;
        //        }
        //    }
        }
    }
}
