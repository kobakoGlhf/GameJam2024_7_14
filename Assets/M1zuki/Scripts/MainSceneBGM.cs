using UnityEngine;

public class MainSceneBGM : MonoBehaviour
{
    [SerializeField] AudioClip[] _clip;
    [SerializeField] AudioSource _audioSource;
    // Start is called before the first frame update
    public void BGMSet()
    {
        _audioSource.clip = _clip[CharaPic._charaNam];
    }
}
