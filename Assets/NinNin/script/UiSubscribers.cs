using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiSubscribers : MonoBehaviour
{
    [SerializeField] Text _text;

    [SerializeField] int _goalCount;
    [SerializeField] int _currentCount = 0;

    [Tooltip("何秒ごとにTextを更新するか")]
    [SerializeField] float _interval = 0.01f;
    [Tooltip("1ループごとに増減する量。")]
    [SerializeField] int _increase = 1;
    [Tooltip("ゴール値と現在値の差の大きさによって、increaseを増減する。\nつまり、差が大きいほど素早く増減する。\nfalseにした場合、CurrentCountの増減は常に一定になる。")]
    [SerializeField] bool _SmoothIncrease = true;
    [Tooltip("差値の何割を、increaseに設定するか。")]
    [SerializeField] float _SmoothIncreasePercent = 0.05f;

    // プロパティ
    /// <summary>外部からこの変数を直接いじって、登録者数を表示する。</summary>
    public int _count
    {
        get => _goalCount;
        set
        {
            if (value < 0)
            {
                Debug.LogWarning("マイナスの値は入力できません");
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
        if (_text != null)
        {
            //currentTextからテキストに表示する
            _text.text ="登録者数 " + _currentCount.ToString() + " 人";
        }
        else
        {
            Debug.LogWarning($"'{_text}' is null.", this.gameObject);
        }
    }

    IEnumerator counter()
    {
        while (true)
        {
            // ゴールと差値によって、_increaseを増減させる
            if (_SmoothIncrease == true)
            {
                float difference;
                difference = Mathf.Abs(_goalCount - _currentCount); //差を入手

                _increase = (int)(difference * _SmoothIncreasePercent);

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
