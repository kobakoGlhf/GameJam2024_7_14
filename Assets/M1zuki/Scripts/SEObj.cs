using UnityEngine;

public class SEObj : MonoBehaviour
{
    AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySe(AudioClip _clip)
    {
        _audioSource.clip = _clip;
        _audioSource.Play();
    }

}
