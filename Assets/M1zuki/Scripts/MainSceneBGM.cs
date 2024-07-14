using UnityEngine;

public class MainSceneBGM : MonoBehaviour
{
    [SerializeField] AudioClip[] _clip;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clip[CharaPic._charaNam];
    }
}
