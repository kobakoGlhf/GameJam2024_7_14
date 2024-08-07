using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesObjectMoves : MonoBehaviour
{
    [SerializeField] Vector2 _endPosition;
    Vector2 _objectStartPos;
    Vector2 _objectPos;
    bool _inCamera=true;
    bool _active = true;
    RectTransform _tf;
    Image _image;
    Text[] _textObject;
    private void Awake()
    {
        _tf = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
        _objectStartPos = _tf.anchoredPosition;
        _textObject = GetComponentsInChildren<Text>();
    }
    public void FadeInOut(bool selected, float waittimer, float movetime,float resetTimer)
    {
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(Move(movetime, waittimer));
            if (selected || !_active)
            {
                StartCoroutine(Select(waittimer));
            }
            _active = !_active;
            if (!_active) StartCoroutine(ChoiceReset(movetime, waittimer, resetTimer));
        }
    }
    private void OnEnable()//リセット
    {
        _tf.anchoredPosition=_objectStartPos;
        _image.color += new Color(0, 0, 0, 1);
        foreach (Text text in _textObject)
        {
            text.color += new Color(0, 0, 0, 1);
        }
        _inCamera=true;
        _active = true;
    }
    IEnumerator Select(float waitTimer)
    {
        float colorCangeTimer = _image.color.a / waitTimer;
        if (_active)
        {
            while (waitTimer > 0)
            {
                waitTimer -= Time.deltaTime;
                _image.color -= new Color(0, 0, 0, colorCangeTimer * Time.deltaTime);
                foreach (Text text in _textObject)
                {
                    text.color -= new Color(0, 0, 0, colorCangeTimer * Time.deltaTime);
                }
                yield return null;
            }
        }
        else
        {
            _image.color += new Color(0, 0, 0, 1);
            foreach (Text text in _textObject)
            {
                text.color += new Color(0, 0, 0, 1);
            }
            yield return new WaitForSeconds(waitTimer);
        }
        yield break;
    }

    IEnumerator Move(float moveTime, float waitTimer)
    {
        float timer = moveTime;
        Vector2 moveVector;
        _objectPos = _tf.anchoredPosition;
        if (!_inCamera)
        {
            moveVector = (_objectStartPos - _objectPos) / moveTime;
        }
        else { moveVector = (_endPosition - _objectPos) / moveTime;}
        yield return new WaitForSeconds(waitTimer);//選んだ選択肢のフェードアウト待ち
        while (timer > 0)
        {
            _tf.anchoredPosition += moveVector * Time.fixedDeltaTime;
            //位置ずれ防止
            if (_tf.anchoredPosition.magnitude <= _objectStartPos.magnitude&&!_inCamera)
            {
                _tf.anchoredPosition =_objectStartPos;
            }
            else if (_tf.anchoredPosition.magnitude>=_endPosition.magnitude&&_inCamera)
            {
                _tf.anchoredPosition=_endPosition;
            }
            timer -= Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        _tf.anchoredPosition = !_inCamera ? _objectStartPos : _endPosition;
        _inCamera = !_inCamera;
        yield break;
    }
    IEnumerator ChoiceReset(float moveTime, float waitMoveTimer,float resetTimer)
    {
        yield return new WaitForSeconds(moveTime+waitMoveTimer+resetTimer+.1f);
        FadeInOut(false, waitMoveTimer, moveTime, resetTimer);
    }
}