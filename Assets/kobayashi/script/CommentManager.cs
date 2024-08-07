using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class CommentManager : MonoBehaviour
{
    [SerializeField,Tooltip("�R�����g�̃v���n�u")] GameObject _commentTextObject;
    [SerializeField,Tooltip("�����ʒu")] RectTransform _poitionAnchor;//�R�����g�ʒu
    [SerializeField]string[] _commentText;
    [SerializeField, Range(200, 1000)] float _commentSpeed;
    [SerializeField] float _createSpeedOrigin;
    float _createSpeed;
    Choices _choices;
    List<GameObject> _creatObject=new List<GameObject>();
    float _timer;
    private void Start()
    {
        _choices = GameObject.FindObjectOfType<Choices>();
        Debug.Log(_choices);
        //_createSpeed=Random.Range(_createSpeedOrigin,_createSpeedOrigin+1);
        _createSpeed = _createSpeedOrigin;
    }
    private void Update()
    {
        if (_choices._inGame==false)
        {
            _timer += Time.deltaTime;
            if (_timer > _createSpeed)//&&!_choices._inGame)
            {
                CreatComment();
                _timer = 0;
                //_createSpeed = Random.Range(_createSpeedOrigin, _createSpeedOrigin + 1);
            }
        }
    }
    public void CreatComment()
    {
        Debug.Log("aaa");
        bool kasu=false;
        int i = Random.Range(0, _commentText.Length);
        GameObject creatObject = Instantiate(_commentTextObject, _poitionAnchor.transform);
        //�K�i��ɃR�����g���\�������悤�ɂ��镔��
        if (_creatObject.Count == 0)
        {
            kasu = true;
        }
        _creatObject.Add(creatObject);
        float creatObjectY = _creatObject.Count * -50;
        RectTransform rectTransform = creatObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(_poitionAnchor.anchoredPosition.x,_poitionAnchor.anchoredPosition.y+ creatObjectY);//�����ʒu
        
        creatObject.GetComponent<Text>().text = _commentText[i];//�R�����g�̒��g�ύX
        StartCoroutine(CommentMove(creatObject,kasu));//�R�����g�𓮂�������
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
