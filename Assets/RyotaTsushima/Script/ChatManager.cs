using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [SerializeField] GameObject _leftUpChat;
    [SerializeField] GameObject _rightUpChat;
    [SerializeField] GameObject _leftDownChat;
    [SerializeField] GameObject _rightDownChat;
    ChatBoxMove _luChat;
    ChatBoxMove _ruChat;
    ChatBoxMove _ldChat;
    ChatBoxMove _rdChat;
    // Start is called before the first frame update
    void Start()
    {
        _luChat = _leftUpChat.GetComponent<ChatBoxMove>();
        _ruChat = _rightUpChat.GetComponent<ChatBoxMove>();
        _ldChat = _leftDownChat.GetComponent<ChatBoxMove>();
        _rdChat = _rightDownChat.GetComponent<ChatBoxMove>();
    }

    // Update is called once per frame
    public void ChatFadeIn()
    {
        _luChat.Fadein();
        _ruChat.Fadein();
        _ldChat.Fadein();
        _rdChat.Fadein();
    }

    public void SelectW()
    {
        _luChat.SelectedEffect();
        _ruChat.FadeOut();
        _ldChat.FadeOut();
        _rdChat.FadeOut();
        
    }

    public void SelectA()
    {
        _luChat.FadeOut();
        _ruChat.SelectedEffect(); 
        _ldChat.FadeOut();
        _rdChat.FadeOut();
    }

    public void SelectS()
    {
        _luChat.FadeOut();
        _ruChat.FadeOut();
        _ldChat.SelectedEffect(); 
        _rdChat.FadeOut();
    }

    public void SelectD()
    {
        _luChat.FadeOut();
        _ruChat.FadeOut();
        _ldChat.FadeOut();
        _rdChat.SelectedEffect(); 
    }
}
