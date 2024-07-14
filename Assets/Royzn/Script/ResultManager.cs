using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public Image _resultImage;
    public Sprite _goldScoreSprite;
    public Sprite _silverScoreSprite;
    public Sprite _copperScoreSprite;
    public Sprite _depressedScoreSprite;
    // Start is called before the first frame update
    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore");
        

        if (finalScore < 30)
        {
            _resultImage.sprite = _depressedScoreSprite;
        }

        else if (finalScore >= 30 && finalScore < 50)
        {
            _resultImage.sprite = _copperScoreSprite;
        }

        else if (finalScore >= 50 && finalScore < 80)
        {
            _resultImage.sprite = _silverScoreSprite;
        }

        else
        {
            _resultImage.sprite = _goldScoreSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
