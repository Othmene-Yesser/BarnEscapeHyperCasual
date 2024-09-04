using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishZone : MonoBehaviour
{
    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(StringsAndConsts.PlayerTag))
        {
            StopCoroutine(gameManager.gameTimeTicker);

            //! Store Coins & Add reached levels
            var coins = PlayerPrefs.GetInt(StringsAndConsts.CoinTag);
            CoinManagerFunctions.StoreCoins(coins + gameManager.Coins);

            Debug.Log("Win");
            gameManager.Win();
        }
    }
    public void StoreStars(int score)
    {
        var levelReched = PlayerPrefs.GetInt(StringsAndConsts.LevelReached);
        var levelNumber = SceneManager.GetActiveScene().buildIndex;
        levelNumber--;

        if (levelNumber >= levelReched)
        {
            PlayerPrefs.SetInt(StringsAndConsts.LevelReached, SceneManager.GetActiveScene().buildIndex);
            if (levelNumber == 0)
            {
                Debug.Log("First saved stars");
                PlayerPrefs.SetInt(StringsAndConsts.StarsCollected,score);
                PlayerPrefs.SetInt(levelNumber.ToString(),score);

                PlayerPrefs.SetInt(StringsAndConsts.LevelReached, SceneManager.GetActiveScene().buildIndex);
                return;
            }
            var stars = PlayerPrefs.GetInt(StringsAndConsts.StarsCollected);
            PlayerPrefs.SetInt(StringsAndConsts.StarsCollected, score + stars);
            PlayerPrefs.SetInt(levelNumber.ToString(), score);
        }
        else
        {
            var stars = PlayerPrefs.GetInt(StringsAndConsts.StarsCollected);
            var starsCollectedForThisLevel = PlayerPrefs.GetInt(levelNumber.ToString());
            stars -= starsCollectedForThisLevel;

            PlayerPrefs.SetInt(StringsAndConsts.StarsCollected, score + stars);
            PlayerPrefs.SetInt(levelNumber.ToString(), score);
        }
        if (levelNumber > levelReched)
        {
            PlayerPrefs.SetInt(StringsAndConsts.LevelReached, SceneManager.GetActiveScene().buildIndex);
        }
    }
}
