using System.Collections;
using System.Collections.Generic;
using CandyCoded.HapticFeedback;
using UnityEngine;

public class PlayerBuffManager : MonoBehaviour
{
    [SerializeField] float speedBuffMaxTime;
    [SerializeField] float timeStopMaxTime;
    [SerializeField] float invisiblityBuffMaxTime;
    public bool SpeedBuff;
    public bool StopTimerBuff;
    public bool Invisible;

    float speedTimer;
    float stopTimer;
    float invisibleTimer;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        SpeedBuff = false;
        StopTimerBuff = false;
        Invisible = false;

        speedTimer = 0;
        stopTimer = 0;
        invisibleTimer = 0;
    }

    private void FixedUpdate()
    {
        if (SpeedBuff && speedTimer <= speedBuffMaxTime)
        {
            speedTimer += Time.deltaTime;
        }
        else if (SpeedBuff) 
        {
            SpeedBuff = false;
        }

        if (StopTimerBuff && stopTimer <= timeStopMaxTime)
        {
            stopTimer += Time.deltaTime;
        }
        else if (StopTimerBuff)
        {
            StopTimerBuff = false;
        }

        if (Invisible && invisibleTimer <= invisiblityBuffMaxTime)
        {
            invisibleTimer += Time.deltaTime;
        }
        else if (Invisible)
        {
            Invisible = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringsAndConsts.SpeedBuff))
        {
            Debug.Log(other.name);
            HapticFeedback.LightFeedback();
            audioManager.PlaySoundEffect(audioManager.collectSpeedBuff);
            Destroy(other.gameObject); //! Insert SFX and VFX
            if (SpeedBuff)
            {
                //! Renew timer for the buff
                speedTimer = 0f;
            }
            else
            {
                SpeedBuff = true;
                speedTimer = 0f;
            }
        }
        else if (other.CompareTag(StringsAndConsts.ZaWarudoBuff))
        {
            Debug.Log(other.name);
            HapticFeedback.LightFeedback();
            audioManager.PlaySoundEffect(audioManager.collectStopTimeBuff);
            Destroy(other.gameObject); //! Insert SFX and VFX
            if (StopTimerBuff)
            {
                //! Renew timer for the buff
                stopTimer = 0f;
            }
            else
            {
                StopTimerBuff = true;
                stopTimer = 0f;
            }
        }
        else if (other.CompareTag(StringsAndConsts.Invisibility))
        {
            Debug.Log(other.name);
            HapticFeedback.LightFeedback();
            audioManager.PlaySoundEffect(audioManager.collectInvisibilityBuff);
            Destroy(other.gameObject); //! Insert SFX and VFX
            if (Invisible)
            {
                //! Renew timer for the buff
                invisibleTimer = 0f;
            }
            else
            {
                Invisible = true;
                invisibleTimer = 0f;
            }
        }
    }
}
