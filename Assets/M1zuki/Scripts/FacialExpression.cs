using UnityEngine;

public class FacialExpression : MonoBehaviour
{
    GameObject _charaSprite;
    SpriteRenderer _spriteRenderer;
    [SerializeField,Tooltip("正解の表情のspriteをアサインしてください")] Sprite _correct;
    [SerializeField,Tooltip("不正解の表情のspriteをアサインしてください")] Sprite _wrong;

    private void Start()
    {
        _charaSprite = GameObject.Find("CharaSprite");
        _spriteRenderer = _charaSprite.GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    public void Correct()
    {
        _spriteRenderer.sprite = _correct;
    }

    public void Wrong()
    {
        _spriteRenderer.sprite = _wrong;
    }
}
