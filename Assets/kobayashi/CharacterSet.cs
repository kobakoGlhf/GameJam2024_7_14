using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSet : MonoBehaviour
{
    //Choices  TalkText  FacialExpression���ݒ肳��Ă�����̂��f�[�^�Ƃ��ēn���Ă�������
    [SerializeField] GameObject[] _characterData;
    [SerializeField]FacialExpression _facialExpression;
    [SerializeField]TalkText _talkManager;
    [SerializeField]CharaSprite _charaSprite;
    [SerializeField]MainSceneBGM _mainSceneBGM;
    [SerializeField] Choices _inGameManager;
    [Space(10)]
    [SerializeField] Text _channelNameText;
    [SerializeField] string[] _channelName=new string[3];
    [Space(10)]
    [SerializeField] Text _videoNameText;
    [SerializeField] string[] _videolName=new string[3];

    private void Awake()
    {
        StartCharaChange(_characterData[CharaPic._charaNam]);
    }
    //�K�v�ȃf�[�^
    //BGM   �����G     �X�g�[���[�̕�����   �I�����̕�����
    void StartCharaChange(GameObject sum)
    {
        _inGameManager?.ArrayInsert(3, 3, 4, sum.GetComponent<Choices>()._choices);
        _talkManager?.TalkTextInArray(sum.GetComponent<TalkText>()._talk);
            var facialDate = sum.GetComponent<FacialExpression>();
        if (facialDate!=null)
        {
            _facialExpression._correct = facialDate._correct;
            _facialExpression._wrong = facialDate._wrong;
        }
        _charaSprite?.SpriteSet();
        _mainSceneBGM?.BGMSet();
        if(_channelNameText!=null) _channelNameText.text = _channelName[CharaPic._charaNam];
        if(_videoNameText!=null) _videoNameText.text = _videolName[CharaPic._charaNam];
    }
}
