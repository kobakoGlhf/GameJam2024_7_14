using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceEfect : MonoBehaviour
{
    //[SerializeField] Choices _choices;
    [SerializeField] Image _panel;
    [SerializeField] Text _text;
    public void AnimationPlay(string choicesString)
    {
        _panel.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);
        _text.text = choicesString;
        _panel.GetComponent<Animator>().Play("PanelFadeIn");
        _text.GetComponent<Animator>().Play("TextFadeIn");

    }
    public void AnimationReset()//アニメーションイベントで呼んでる
    {
        _panel.color = new Color(default, default, default, 0);
        _text.color= new Color(default, default, default, 0);
        _panel.gameObject.SetActive(false);
        _text.gameObject.SetActive(false);
    }
}
