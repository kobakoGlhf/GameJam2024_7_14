using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ResultCheck : MonoBehaviour
{
    public int _score;

    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddScore(int _points)
    {
        _score += _points;
    }
    public void GoToResultScreen()
    {
        PlayerPrefs.SetInt("FinalScore", _score); // スコアを保存
        SceneManager.LoadScene("ResultScene"); // リザルトシーンに遷移
    }
}
