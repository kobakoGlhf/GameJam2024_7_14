using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    Choices _choices;
    public static int _score;
    private void Start()
    {
        _choices = GetComponent<Choices>();
    }
    public void AddScore(int times, int correct1, int correct2, int correct3)   //スコア加算　正解のインデックスを入力してください
    {
        if (_choices._selectedChoices[times * 3] == _choices._choices[correct1]
            && _choices._selectedChoices[times * 3 + 1] == _choices._choices[correct2]
            && _choices._selectedChoices[times * 3 + 2] == _choices._choices[correct3])
        {
            _score++;
            //StartCoroutine(GoodEffect());
        }
        else
        {
            //StartCoroutine(BadEffect());
        }
        Debug.Log(_score);
    }
}
