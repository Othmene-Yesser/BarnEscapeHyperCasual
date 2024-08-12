using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    PlayerBuffManager buffsManager;
    ScoreManager scoreManager;

    Slider slider;
    [HideInInspector] public BoxCollider winZone;
    GameObject pauseMenu;

    [HideInInspector] public float gameTime;

    private int coins;
    private bool paused;

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

    Door door;

    public IEnumerator gameTimeTicker;

    private void Awake()
    {
        door = FindObjectOfType<Door>();
        slider = FindObjectOfType<Slider>();
        winZone = FindObjectOfType<FinishZone>().GetComponent<BoxCollider>();
        mobileInput = FindObjectOfType<FloatingJoystick>();
        inputManager = FindObjectOfType<InputManager>();
        buffsManager = FindObjectOfType<PlayerBuffManager>();
        pauseMenu = transform.GetChild(0).gameObject;
        scoreManager = gamePanel.GetComponent<ScoreManager>();
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        gameTime = levelTime;
        gameTimeTicker = TimeTicker();
        StartCoroutine(gameTimeTicker);
        coinText.text = "0";
        paused = false;
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
            //TODO Add Door Animation
            door.OpenDoor();
        }
    }
    IEnumerator TimeTicker()
    {
        while (gameTime > 0f)
        {
            if (buffsManager.StopTimerBuff)
            {
                yield return null;
                continue;
            }
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
            scoreManager.lostByAllies = true;
            Lose();
        }
    }
    public void Win()
    {
        inputManager.immobile = true;
        mobileInput.gameObject.SetActive(false);
        StopAllSeeker();
        winZone.enabled = false;
        //! Play animation winning
        gamePanel.SetActive(true);
        scoreManager.DisplayScore();
    }
    public void Lose()
    {
        inputManager.immobile = true;
        mobileInput.gameObject.SetActive(false);
        StopAllSeeker();
        winZone.enabled = false;

        //! Play animation losing
        gamePanel.SetActive(true);
        scoreManager.DisplayScore();
    }

    public void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
        paused = !paused;
        pauseMenu.SetActive(paused);
    }
}
