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
        _score = 0;
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
            if (_score == 1)
            {
                _subscribers = 10000;
            }
            else if (_score == 2)
            {
                _subscribers = 100000;
            }
            else if (_score >= 3)
            {
                _subscribers = 1000000;
            }
            _subscribersUi._count = _subscribers;
            Debug.Log("スコアを受け取りました");
            Debug.Log("Score:" + _score);
        }
    }
    public void AddScore()   //スコア加算　正解のインデックスを入力してください
    {
        int select1 = 0, select2 = 0, select3 = 0;
        int i = 0;
        foreach (var choice in _choices._choicesNeo)
        {
            if (choice == _choices._selectedChoices[0])
            {
                select1 = i;
            }
            else if (choice == _choices._selectedChoices[1])
            {
                select2 = i;
            }
            else if (choice == _choices._selectedChoices[2])
            {
                select3 = i;
            }
            i++;
        }
        if (select1 - select2 == select2 - select3 && !(select1 - select2 == 0))
        {
            _score++;
            if (_score == 1)
            {
                _subscribers = 10000;
            }
            else if (_score == 2)
            {
                _subscribers = 100000;
            }
            else if (_score >= 3)
            {
                _subscribers = 1000000;
            }
            _subscribersUi._count = _subscribers;
            Debug.Log("スコアを受け取りました");
            Debug.Log("Score:" + _score);
        }
    }
}
