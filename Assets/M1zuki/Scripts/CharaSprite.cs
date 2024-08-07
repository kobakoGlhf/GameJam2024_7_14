using UnityEngine;
using UnityEngine.UI;

public class CharaSprite : MonoBehaviour
{

    [SerializeField] Sprite[] _charaSprites;
    [SerializeField] Image _spriteRenderer;

    // Start is called before the first frame update

    public void SpriteSet()
    {
        if (CharaPic._charaNam == 0)
        {
            _spriteRenderer.sprite = _charaSprites[0]; //éGík
        }
        else if (CharaPic._charaNam == 1)
        {
            _spriteRenderer.sprite = _charaSprites[1]; //ÉQÅ[ÉÄ
        }
        else
        {
            _spriteRenderer.sprite = _charaSprites[2]; //è§ïi
        }
    }
}
