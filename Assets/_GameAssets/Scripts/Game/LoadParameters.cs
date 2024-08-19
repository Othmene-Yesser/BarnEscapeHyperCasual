using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadParameters : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    private void Start()
    {
        bool isActive = PlayerPrefs.GetInt("MasterVolumeSaved") == 1;
        if (isActive)
        {
            audioMixer.SetFloat("MasterVolume", 0);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", -80);
        }
    }
}
