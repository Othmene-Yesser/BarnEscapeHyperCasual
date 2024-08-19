using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------Audio Sources----------")]
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource SFX;

    [Header("----------Audio Clips----------")]
    public AudioClip music;
    public AudioClip menuClick;
    public AudioClip collectCoin;
    public AudioClip collectAlly;
    public AudioClip collectSpeedBuff;
    public AudioClip collectInvisibilityBuff;
    public AudioClip collectStopTimeBuff;
    public AudioClip star;
    public AudioClip lose;
    public AudioClip win;

    private void Start()
    {
        Music.clip = music;
        Music.Play();
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        SFX.PlayOneShot(clip);
    }

    public void MenuClickButton()
    {
        SFX.PlayOneShot(menuClick);
    }
}
