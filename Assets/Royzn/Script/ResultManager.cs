using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
   
    public Image _resultImage;
    public Image _resultPopImage;

    public Sprite _goldScoreSpriteX;
    public Sprite _goldPopSprX;
    public Sprite _silverScoreSpriteX;
    public Sprite _silverPopSprX;
    public Sprite _copperScoreSpriteX;
    public Sprite _copperPopSprX;
    public Sprite _depressedScoreSpriteX;
    public Sprite _depressedPopSprX;

    public Sprite _goldScoreSpriteY;
    public Sprite _goldPopSprY;
    public Sprite _silverScoreSpriteY;
    public Sprite _silverPopSprY;
    public Sprite _copperScoreSpriteY;
    public Sprite _copperPopSprY;
    public Sprite _depressedScoreSpriteY;
    public Sprite _depressedPopSprY;

    public Sprite _goldScoreSpriteZ;
    public Sprite _goldPopSprZ;
    public Sprite _silverScoreSpriteZ;
    public Sprite _silverPopSprZ;
    public Sprite _copperScoreSpriteZ;
    public Sprite _copperPopSprZ;
    public Sprite _depressedScoreSpriteZ;
    public Sprite _depressedPopSprZ;
    // Start is called before the first frame update
    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore");

        //スコアの数値は（仮）です。

        switch (CharaPic._charaNam)
        {
            case 0:
                if (finalScore < 30)
                {
                    _resultImage.sprite = _depressedScoreSpriteX;
                    _resultPopImage.sprite = _depressedPopSprX;
                }

                else if (finalScore >= 30 && finalScore < 50)
                {
                    _resultImage.sprite = _copperScoreSpriteX;
                    _resultPopImage.sprite = _copperPopSprX;
                }

                else if (finalScore >= 50 && finalScore < 80)
                {
                    _resultImage.sprite = _silverScoreSpriteX;
                    _resultPopImage.sprite = _silverPopSprX;
                }

                else
                {
                    _resultImage.sprite = _goldScoreSpriteX;
                    _resultPopImage.sprite = _goldPopSprX;
                }
                break;

                case 1:
                if (finalScore < 30)
                {
                    _resultImage.sprite = _depressedScoreSpriteY;
                    _resultPopImage.sprite = _depressedPopSprY;
                }

                else if (finalScore >= 30 && finalScore < 50)
                {
                    _resultImage.sprite = _copperScoreSpriteY;
                    _resultPopImage.sprite = _copperPopSprY;
                }

                else if (finalScore >= 50 && finalScore < 80)
                {
                    _resultImage.sprite = _silverScoreSpriteY;
                    _resultPopImage.sprite = _silverPopSprY;
                }

                else
                {
                    _resultImage.sprite = _goldScoreSpriteY;
                    _resultPopImage.sprite = _goldPopSprY;
                }
                break;

                case 2:
                if (finalScore < 30)
                {
                    _resultImage.sprite = _depressedScoreSpriteZ;
                    _resultPopImage.sprite = _depressedPopSprZ;
                }

                else if (finalScore >= 30 && finalScore < 50)
                {
                    _resultImage.sprite = _copperScoreSpriteZ;
                    _resultPopImage.sprite = _copperPopSprZ;
                }

                else if (finalScore >= 50 && finalScore < 80)
                {
                    _resultImage.sprite = _silverScoreSpriteZ;
                    _resultPopImage.sprite = _silverPopSprZ;
                }

                else
                {
                    _resultImage.sprite = _goldScoreSpriteZ;
                    _resultPopImage.sprite = _goldPopSprZ;
                }
                break;

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
