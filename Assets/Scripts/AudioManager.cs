using System.Threading;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    public AudioClip background;
    public AudioClip good;
    public AudioClip bad;
    public AudioClip firstFind;
    public AudioClip endGame;
    public AudioClip metal;
    public AudioClip plastic;
    public AudioClip glass;
    public AudioClip paper;
    public AudioClip cardboard;
    public AudioClip compost;
    public AudioClip trash;

    public bool mute = false;



    private void Start()
    {
        PlayMusic(background);
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void ToggleMute()
    {
        mute = !mute;
        SFXSource.mute = mute;
        musicSource.mute = mute;
    }
}
