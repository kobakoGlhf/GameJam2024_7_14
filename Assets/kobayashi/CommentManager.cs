using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class CommentManager : MonoBehaviour
{
    [SerializeField,Range(200,1000)] float _commentSpeed;
    [SerializeField,Tooltip("コメントのプレハブ")] GameObject _commentTextObject;
    [SerializeField,Tooltip("生成位置")] RectTransform _poitionAnchor;
    [SerializeField]string[] _commentText;
    bool _inGame;
    public List<GameObject> _creatObject=new List<GameObject>();
    float _timer;
    private void Update()
    {
        _timer= Time.deltaTime;

    }
    public void CreatComment()
    {
        bool kasu=false;
        int i = Random.Range(0, _commentText.Length);
        GameObject creatObject = Instantiate(_commentTextObject, _poitionAnchor.transform);
        if (_creatObject.Count == 0)
        {
            Debug.Log("aaa");
            kasu = true;
        }
        _creatObject.Add(creatObject);
        Debug.Log(_creatObject.Count);
        float creatObjectY = _creatObject.Count * -50;
        RectTransform rectTransform = creatObject.GetComponent<RectTransform>();
        creatObject.GetComponent<Text>().text=_commentText[i];
        rectTransform.anchoredPosition = new Vector2(_poitionAnchor.anchoredPosition.x,_poitionAnchor.anchoredPosition.y+ creatObjectY);
        StartCoroutine(CommentMove(creatObject,kasu));
    }
    IEnumerator CommentMove(GameObject gameObject,bool kasu)
    {
        bool aaa=true;
        while (true)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition += new Vector2(_commentSpeed * -1, 0) * Time.deltaTime;
            if (rectTransform.anchoredPosition.x < -1920 / 2 && kasu&&aaa) 
            {
                aaa = false;
                _creatObject.Clear();
            }
            if (rectTransform.anchoredPosition.x < -2500)
            {
                Destroy(rectTransform.gameObject);
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
