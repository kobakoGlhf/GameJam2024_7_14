using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Score _scores;
    int _score;
    // Start is called before the first frame update
    void Start()
    {
        _scores = gameObject.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddScore()
    {
        _scores.AddScore(0, 0, 4, 8);
        Debug.Log(_score);
    }
}
