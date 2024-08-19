using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    AudioManager audioManager;

    private void OnEnable()
    {
        audioManager= FindObjectOfType<AudioManager>();
        audioManager.PlaySoundEffect(audioManager.star);
    }
}
