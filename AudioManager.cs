using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-------- Audio Source -------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-------- Audio clip -------")]
    public AudioClip background;
    public AudioClip jump;
    public AudioClip death;

    public void PlayMusic()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
