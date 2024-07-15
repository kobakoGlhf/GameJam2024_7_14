using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCount : MonoBehaviour
{
    int _goalCount;
    int _currentCount = 0;

    [SerializeField] Text _text;
    /// <summary>何秒ごとにTextを更新するか</summary>
    [SerializeField] float _interval = 0.01f;
    /// <summary>1ループごとに増減する量</summary>
    [SerializeField] int _increase = 1;

    /// <summary>外部からこの変数を直接いじって、登録者数を表示する。</summary>
    public int _subscribers
    {
        get => _goalCount;
        set
        {
            if (value < 0)
            {
                Debug.Log("マイナスの値は入力できません");
                return;
            }
            _goalCount = value;
        }
    }


    private void Start()
    {
        StartCoroutine(counter()); //コルーチンを開始
    }
    private void Update()
    {
        //currentTextからテキストに表示する
        _text.text = _currentCount.ToString() + " 人";
    }

    IEnumerator counter()
    {
        while (true)
        {
            // ゴールとの離れ具合によって、_increaseを増減させる
            {
                int difference;
                difference = Mathf.Abs(_goalCount - _currentCount); //差を入手

                _increase = difference / 10;

                if (_increase <= 0) //最低値は1
                {
                    _increase = 1;
                }
            }

            // currentCountを、goalCountから、だんだんと増やす
            // 増やす
            if (_currentCount < _goalCount)
            {
                int tmp = _currentCount + _increase;//お試しで計算

                // もし、過剰に増えることがないなら..
                if (tmp < _goalCount)
                {
                    _currentCount += _increase;
                }
                //もし、過剰に増えすぎるなら..
                else
                {
                    _currentCount = _goalCount;
                }
            }
            // 減らす
            else if(_currentCount > _goalCount)
            {
                int tmp = _currentCount - _increase;//お試しで計算

                // もし、過剰に減らすことがないなら..
                if (tmp > _goalCount)
                {
                    _currentCount -= _increase;
                }
                //もし、過剰に減らしすぎるなら..
                else
                {
                    _currentCount = _goalCount;
                }
            }

            // インターバル分、待つ
            yield return new WaitForSeconds(_interval);
        }
    }

}
