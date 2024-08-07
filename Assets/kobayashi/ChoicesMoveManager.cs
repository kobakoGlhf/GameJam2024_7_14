using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChoicesMoveManager : MonoBehaviour
{
    [SerializeField] ChoicesObjectMoves _w;
    [SerializeField] ChoicesObjectMoves _a;
    [SerializeField] ChoicesObjectMoves _s;
    [SerializeField] ChoicesObjectMoves _d;
    [SerializeField] float _waitMoveTimer;
    [SerializeField] float _movingTime;
    [SerializeField] float _waitResetTimer;
    public float _animetionTimer
    {
        get { return (_waitMoveTimer + _movingTime)*2+_waitResetTimer; }
    }
    Dictionary<char,ChoicesObjectMoves> _choiceskey=new Dictionary<char, ChoicesObjectMoves>();
    private void Start()
    {
        _choiceskey.Add('W', _w);
        _choiceskey.Add('A', _a);
        _choiceskey.Add('S', _s);
        _choiceskey.Add('D', _d);

    }
    public void hideChoices()
    {
        foreach(KeyValuePair<char,ChoicesObjectMoves> choise in _choiceskey)
        {
            choise.Value.gameObject.SetActive(!choise.Value.gameObject.activeSelf);
        }
    }
    public void SelectKey(char pushKey)//テスト用
    {
        foreach (KeyValuePair<char,ChoicesObjectMoves> choicesMove in _choiceskey)
        {
            if(choicesMove.Key == pushKey)
            {
                choicesMove.Value?.FadeInOut(true, _waitMoveTimer, _movingTime,_waitResetTimer);
            }
            else choicesMove.Value?.FadeInOut(false, _waitMoveTimer, _movingTime, _waitResetTimer);
        }
    }
}
