using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("MasterVolumeSaved"))
        {
            bool isActive = PlayerPrefs.GetInt("MasterVolumeSaved") == 1;
            Toggle toggle = GetComponent<Toggle>();
            toggle.isOn = isActive;
        }
    }

    public void UpdateVolumeVariable(bool isActive)
    {
        //! Set the audio
        if (isActive)
        {
            audioMixer.SetFloat("MasterVolume", 0);
            PlayerPrefs.SetInt("MasterVolumeSaved", 1);
        }
        else
        {
            audioMixer.SetFloat("MasterVolume", -80);
            PlayerPrefs.SetInt("MasterVolumeSaved", 0);
        }
    }
}
