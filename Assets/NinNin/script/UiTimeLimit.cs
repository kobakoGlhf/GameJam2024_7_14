using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UiTimeLimit : MonoBehaviour
{
    [SerializeField] Image _circle;
    [SerializeField] Text _text;

    [SerializeField] float _currentTime = 1.0f;
    [SerializeField] float _maxTime = 3.0f;

    bool _isInstantiate = false;

    Queue<GameObject> _createdTimer=new Queue<GameObject>();
    public static bool _timerStop;

    public static GameObject _recentObject;
    private void Start()
    {
        if (_isInstantiate)
        {
            _recentObject = gameObject;
        }
        _timerStop = true;
    }
    void Update()
    {
        if (_isInstantiate&&_timerStop)
        {
            // タイマー減算
            _currentTime -= Time.deltaTime;

            // UIに表示
            // エラーチェック
            if (_circle == null)
            {
                Debug.LogWarning($"'{_circle}' is null.", this.gameObject);
            }
            if (_text == null)
            {
                Debug.LogWarning($"'{_text}' is null.", this.gameObject);
            }
            // 主処理。nullなし。
            else
            {
                // サークルゲージ
                _circle.fillAmount = _currentTime / _maxTime;

                // テキスト
                _text.text = _currentTime.ToString("0.##") + "s";

                // 破壊
                if (_currentTime <= 0.0f)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

    /// <summary>
    /// 外部からこの関数を呼び出して、タイマー表示を開始。
    /// </summary>
    /// <param name="time">何秒のタイマー？</param>
    /// <param name="pos">どこに表示する？</param>
    /// /// <param name="canvas">親となるキャンバス</param>
    public void Display(GameObject originalObject,float time, Vector2 pos, GameObject canvas)
    {
        GameObject obj;
        obj = Instantiate(originalObject, pos, Quaternion.identity, canvas.transform); //インスタンス作成
        //設定項目
        obj.SetActive(true); //有効にする
        UiTimeLimit uiTimeLimit = obj.GetComponent<UiTimeLimit>();
        uiTimeLimit._isInstantiate = true; //インスタンスモード

        //初期値を入力
        uiTimeLimit._maxTime = time;
        uiTimeLimit._currentTime = time;

        //追加　delete用に生成したobjをQueueに入れる
        _createdTimer.Enqueue(obj);
    }
    public void DestroyTimer()
    {
        if (_createdTimer.Count != 0)
        {
            Destroy(_createdTimer.Dequeue());
        }
    }
    IEnumerator FadeOut(float timer)
    {
        var image= GetComponent<Image>();
        float colorCangeTimer = image.color.a / timer;
        while (timer > 0&& image != null)
        {
            timer -= Time.deltaTime;
            image.color -= new Color(0, 0, 0, colorCangeTimer * Time.deltaTime);
            _text.color -= new Color(0, 0, 0, colorCangeTimer * Time.deltaTime);
            yield return null;
        }
    }
}
