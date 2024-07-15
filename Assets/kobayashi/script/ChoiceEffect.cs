using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceEffect : MonoBehaviour
{
    //[SerializeField] Choices _choices;
    [SerializeField] Image _panel;
    [SerializeField] public Text _text;
    private void Start()
    {
        _panel.gameObject.SetActive(true);
        _text.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
    }
    public void AnimationPlay(string choicesString)
    {
        this.gameObject.SetActive(true);
        _text.text = choicesString;
        _panel.GetComponent<Animator>().Play("PanelFadeIn");
        _text.GetComponent<Animator>().Play("TextFadeIn");
        Invoke("AnimationReset1", 2f);

    }
    public void AnimationReset1()//アニメーションイベントで呼んでる
    {
        _panel.color = new Color(default, default, default, 0);
        _text.color= new Color(default, default, default, 0);
        this.gameObject.SetActive(false);
    }
}
