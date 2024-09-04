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
    public int DisplayScore()
    {
        int score = CheckScore();
        
        if (lostByAllies)
        {
            animator.SetInteger(StringsAndConsts.Score, 0);
            animator.SetBool("Win", false);
        }
        else
        {
            animator.SetBool("Win", score > 0);
            animator.SetInteger(StringsAndConsts.Score, score);
        }
        if (animator.GetInteger(StringsAndConsts.Score) == 0)
        {
            animator.SetInteger(StringsAndConsts.Score, -1);
        }
        return score;
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

        if (!playerManager.isAlive)
            return 0;
        return (gameManager.gameTime<=0)?0:(stars <= 0) ? 1 : stars;
    }
}
