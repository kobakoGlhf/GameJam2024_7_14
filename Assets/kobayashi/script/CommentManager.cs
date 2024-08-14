using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommentManager : MonoBehaviour
{
    [SerializeField, Tooltip("コメントのプレハブ")] GameObject _commentTextPrefab;
    [SerializeField, Tooltip("生成位置")] RectTransform _positionAnchor;//コメント位置
    [SerializeField] string[] _commentText;
    [SerializeField, Range(200, 1000)] float _commentSpeed;
    [SerializeField] public float _createSpeed;
    [SerializeField] int _createCommentMaxHigh;
    Choices _choices;
    List<GameObject> _creatObject = new List<GameObject>();
    [SerializeField] float _space;
    float _timer;
    float _commentHigh;
    int _callClearCountFlag;
    private void Start()
    {
        _choices = GameObject.FindObjectOfType<Choices>();
        var commentRectTf = _commentTextPrefab.GetComponent<RectTransform>();
        _commentHigh = commentRectTf.sizeDelta.y + _space;
        CreatComment();
    }
    private void Update()
    {
        if (_choices?._inGame == false || _choices == null)
        {
            _timer += Time.deltaTime;
            if (_timer > _createSpeed)
            {
                CreatComment();
                _timer = 0;

            }
        }
    }
    public void CreatComment()
    {
        CommentResetMode mode;
        int i = Random.Range(0, _commentText.Length);
        GameObject creatObject = Instantiate(_commentTextPrefab, _positionAnchor.transform);
        //階段状にコメントが表示されるようにする部分
        if (_creatObject.Count == 0)
        {
            mode = CommentResetMode.Center;
        }
        else
        {
            mode = CommentResetMode.Normal;
        }
        _creatObject.Add(creatObject);
        float creatObjectY = (_creatObject.Count - 1) * -_commentHigh;
        RectTransform rectTransform = creatObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0+ creatObjectY);//初期位置

        creatObject.GetComponent<Text>().text = _commentText[i];//コメントの中身変更
        StartCoroutine(CommentMove(creatObject, mode));//コメントを動かす部分
    }
    IEnumerator CommentMove(GameObject gameObject, CommentResetMode resetMode)
    {
        var text=gameObject.GetComponent<Text>();
        var textOutLine=gameObject.GetComponent<Outline>();
        bool flag = false;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        if (resetMode == CommentResetMode.Center)
        {
            flag = true;
        }
        while (true)
        {
            rectTransform.anchoredPosition += new Vector2(_commentSpeed * -1, 0) * Time.deltaTime;
            if (rectTransform.anchoredPosition.x < -1920 / 2 && resetMode == CommentResetMode.Center && flag)
            {
                flag = false;
                _creatObject.Clear();
            }
            else if (_creatObject.Count == _createCommentMaxHigh && resetMode == CommentResetMode.Center && flag)
            {
                _creatObject.Clear();
                flag = false;
            }
            if (rectTransform.anchoredPosition.x < -2500)
            {
                Destroy(gameObject);
                yield break;
            }
            if (_choices?._inGame == true)
            {
                text.color -= new Color(0, 0, 0, 3f/255);
                textOutLine.effectColor -= new Color(0, 0, 0, 3f / 255);
                if (text.color.a <= 0)
                {
                    Destroy(gameObject);
                    yield break;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    enum CommentResetMode
    {
        Normal,
        Center,
        CommentLimit
    }
}
