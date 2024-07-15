using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class ChatBoxMove : MonoBehaviour
{
    [SerializeField] float _moveTime;
    //キャンバスの座標ではなく２Dの座標を入力してください
    [SerializeField, Tooltip("出てくる場所のX座標、キャンバスの座標ではなく２Dの座標を入力してください")] float _moveX;
    [SerializeField, Tooltip("出てくる場所のY座標、キャンバスの座標ではなく２Dの座標を入力してください")] float _moveY;
    float _startX;
    float _startY;
    float _x;
    float _y;
    float _dX;
    float _dY;
    RectTransform _tf;
    // Start is called before the first frame update
    void Start()
    {
        _tf = GetComponent<RectTransform>();
        _startX = _tf.position.x;
        _startY = _tf.position.y;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fadein()
    {
        StartCoroutine(InMoves());
    }

    public void FadeOut()
    {
        StartCoroutine(OutMoves());
    }

    public void SelectedEffect()
    {
        gameObject.SetActive(false);
        _tf.position = new Vector2(_startX, _startY);
        gameObject.SetActive(true);
    }

    IEnumerator InMoves()
    {
        float timer = _moveTime;
        _x = gameObject.transform.position.x;
        _y = gameObject.transform.position.y;
        _dX = (_moveX - _x) / _moveTime;
        _dY = (_moveY - _y) / _moveTime;
        while (timer > 0)
        {
            _x += _dX * Time.deltaTime;
            _y += _dY * Time.deltaTime;
            _tf.position = new Vector2(_x, _y);
            timer -= Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    IEnumerator OutMoves()
    {
        float timer = _moveTime;
        _x = gameObject.transform.position.x;
        _y = gameObject.transform.position.y;
        _dX = (_startX - _x) / _moveTime;
        _dY = (_startY - _y) / _moveTime;
        while (timer >= 0)
        {
            _x += _dX * Time.deltaTime;
            _y += _dY * Time.deltaTime;
            _tf.position = new Vector2(_x, _y);
            timer -= Time.deltaTime;
            yield return null;
        }
        yield break;
    }
}