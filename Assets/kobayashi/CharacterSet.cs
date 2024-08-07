using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSet : MonoBehaviour
{
    //Choices  TalkText  FacialExpression���ݒ肳��Ă�����̂��f�[�^�Ƃ��ēn���Ă�������
    [SerializeField] GameObject[] _characterData;
    [SerializeField]FacialExpression _facialExpression;
    [SerializeField]TalkText _talkManager;
    [SerializeField]CharaSprite _charaSprite;
    [SerializeField]MainSceneBGM _mainSceneBGM;
    [SerializeField] Choices _inGameManager;
    private void Awake()
    {
        //StartCharaChange(_characterDate[CharaPic._charaNam]); 
        StartCharaChange(_characterData[CharaPic._charaNam]);

    }
    //�K�v�ȃf�[�^
    //BGM   �����G     �X�g�[���[�̕�����   �I�����̕�����
    void StartCharaChange(GameObject data)
    {
        _inGameManager.ArrayInsert(3, 3, 4, data.GetComponent<Choices>()._choices);
        _talkManager.TalkTextInArray(data.GetComponent<TalkText>()._talk);
        var facialDate=data.GetComponent<FacialExpression>();
        _facialExpression._correct=facialDate._correct;
        _facialExpression._wrong=facialDate._wrong;
        _charaSprite.SpriteSet();
        _mainSceneBGM.BGMSet();

    }
}
