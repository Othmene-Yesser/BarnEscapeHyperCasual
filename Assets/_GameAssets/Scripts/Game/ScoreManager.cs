using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameObject[] stars;
    GameManager gameManager;
    TextMeshProUGUI time;
    PlayerManager playerManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        time = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        playerManager = FindObjectOfType<PlayerManager>();
    }
    private void OnEnable()
    {
        DisplayScore();
    }

    private void DisplayScore()
    {
        int score = CheckScore();

        for (int i = 0; i < stars.Length; i++)
        {
            if (i >= score)
            {
                stars[i].SetActive(false);
            }
        }
    }

    private int CheckScore()
    {
        int stars = 3;
        for (int i = 0; i < gameManager.allies.Length; i++)
        {
            if (gameManager.allies[i].alive == false)
            {
                stars--;
                break;
            }
        }
        
        float timePercentage = gameManager.gameTime / gameManager.levelTime;

        if (timePercentage < 0.6f)
        {
            
            stars--;
            if (timePercentage < 0.3f)
            {
                stars--;
            }
        }
        string _time = (Mathf.Abs(gameManager.gameTime)).ToString();
        string timerForGame = "Time remaining : " + _time[0] + _time[1] + _time[2] + _time[3] + "s";
        time.text = timerForGame;
        if (!playerManager.isAlive)
            return 0;
        return (gameManager.gameTime<=0)?0:(stars <= 0) ? 1 : stars;
    }
}
