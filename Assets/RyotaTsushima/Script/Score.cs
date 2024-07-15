using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    Choices _choices;
    FacialExpression _faialExpression;
    [SerializeField] UiSubscribers _subscribersUi;
    public static int _score;
    int _subscribers;

    private void Start()
    {
        _choices = GetComponent<Choices>();
        _faialExpression = GetComponent<FacialExpression>();
    }
    public void AddScore(int times, int correct1, int correct2, int correct3)   //スコア加算　正解のインデックスを入力してください
    {
        if (_choices._selectedChoices[times * 3] == _choices._choices[correct1]
            && _choices._selectedChoices[times * 3 + 1] == _choices._choices[correct2]
            && _choices._selectedChoices[times * 3 + 2] == _choices._choices[correct3])
        {
            _score++;
            _faialExpression.Correct();

            //StartCoroutine(GoodEffect());
            if (_score == 1)
            {
                _subscribers = 10000;
            }
            else if (_score == 2)
            {
                _subscribers = 100000;
            }
            else if(_score >= 3)
            {
                _subscribers = 1000000;
            }
            _subscribersUi._count = _subscribers;
            Debug.Log("スコアを受け取りました");
            Debug.Log("Score:" + _score);
        }
        else
        {
            _faialExpression.Wrong();
            //StartCoroutine(BadEffect());
        }


    }
}
