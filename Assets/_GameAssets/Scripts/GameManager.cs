using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject gamePanel;
    [SerializeField] float percentageToLose = 0.5f;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] SeekerAI[] seekers;
    public AllyAI[] allies;
    public float levelTime = 120f;

    FloatingJoystick mobileInput;
    InputManager inputManager;

    Slider slider;
    BoxCollider winZone;

    [HideInInspector] public float gameTime;

    private int coins;

    public int Coins
    {
        get
        {
            return coins;
        }
        set
        {
            coins = value;
            coinText.text = coins.ToString();
        }
    }

    GameObject door;

    IEnumerator gameTimeTicker;

    private void Awake()
    {
        door = FindObjectOfType<Door>().gameObject;
        slider = FindObjectOfType<Slider>();
        winZone = GetComponent<BoxCollider>();
        mobileInput = FindObjectOfType<FloatingJoystick>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Start()
    {
        gameTime = levelTime;
        gameTimeTicker = TimeTicker();
        StartCoroutine(gameTimeTicker);
        coinText.text = "0";
    }

    public void CheckIfCollectedAllALlies()
    {
        int i = 0;
        int arrayLength = allies.Length;
        while ((i < arrayLength && allies[i].hasBeenCaptured))
        {
            i++;
        }
        if (i == arrayLength)
        {
            //! Open Door
            door.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Strings.PlayerTag))
        {
            Debug.Log("Win");
            StopCoroutine(gameTimeTicker);
            Win();
        }
    }

    IEnumerator TimeTicker()
    {
        while (gameTime > 0f)
        {
            gameTime -= Time.deltaTime;
            slider.value = gameTime / levelTime;
            yield return null;
        }
        Debug.Log("Lost");
        Lose();
    }

    private void StopAllSeeker()
    {
        for (int i = 0; i < seekers.Length; i++)
        {
            seekers[i].Idle();
        }
    }
    public void CheckIfLostManyAllies()
    {
        float j = allies.Length;
        for (int i = 0; i < allies.Length; i++)
        {
            if (allies[i].alive == false)
            {
                j--;
            }
        }
        if ((j / allies.Length) < percentageToLose)
        {
            Debug.Log("Lost By Allies deaths");
            StopCoroutine(gameTimeTicker);
            Lose();
        }
    }
    private void Win()
    {
        inputManager.immobile = true;
        mobileInput.gameObject.SetActive(false);
        StopAllSeeker();
        winZone.enabled = false;
        gamePanel.SetActive(true);
        gamePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Winner";
    }
    private void Lose()
    {
        inputManager.immobile = true;
        mobileInput.gameObject.SetActive(false);
        StopAllSeeker();
        winZone.enabled = false;
        gamePanel.SetActive(true);
        gamePanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Loser";
    }

}
