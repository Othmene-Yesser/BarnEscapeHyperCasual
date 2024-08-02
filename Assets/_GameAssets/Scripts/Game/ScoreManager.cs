using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    GameManager gameManager;
    PlayerManager playerManager;
    Animator animator;

    public bool lostByAllies;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        animator = GetComponent<Animator>();
    }
    public void DisplayScore()
    {
        int score = CheckScore();
        
        if (lostByAllies)
        {
            animator.SetInteger(Strings.Score, 0);
            animator.SetBool("Win", false);
        }
        else
        {
            animator.SetBool("Win", score > 0);
            animator.SetInteger(Strings.Score, score);
        }
        if (animator.GetInteger(Strings.Score) == 0)
        {
            animator.SetInteger(Strings.Score, -1);
        }
    }

    private int CheckScore()
    {
        int stars = 3;
        int auxStars = 3;
        for (int i = 0; i < gameManager.allies.Length; i++)
        {
            if (stars <= 2)
            {
                auxStars--;
                continue;
            }
            if (gameManager.allies[i].alive == false)
            {
                stars--;
                auxStars--;
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

        if (!playerManager.isAlive)
            return 0;
        return (gameManager.gameTime<=0)?0:(stars <= 0) ? 1 : stars;
    }
}
