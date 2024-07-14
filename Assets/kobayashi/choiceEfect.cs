using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceEfect : MonoBehaviour
{
    //[SerializeField] Choices _choices;
    [SerializeField] Image _panel;
    [SerializeField] GameObject _textObject;
    Text _text;
    private void Start()
    {
        _text = _textObject.GetComponent<Text>();
    }
    public void AnimationPlay()
    {
        _panel.GetComponent<Animator>().Play("PanelFadeIn");
        _textObject.GetComponent<Animator>().Play("TextFadeIn");

    }
    public void AnimationReset()
    {
        _panel.color = new Color(default, default, default, 0);
        _text.color= new Color(default, default, default, 0);
    }
}
