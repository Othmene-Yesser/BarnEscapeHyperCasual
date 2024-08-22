using System.Collections;
using System.Collections.Generic;
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
        if (other.CompareTag(Strings.PlayerTag))
        {
            StopCoroutine(gameManager.gameTimeTicker);

            //! Store Coins & Add reached levels
            var coins = PlayerPrefs.GetInt(Strings.CoinTag);
            CoinManagerFunctions.StoreCoins(coins + gameManager.Coins);

            PlayerPrefs.SetInt(Strings.LevelReached, SceneManager.GetActiveScene().buildIndex);

            Debug.Log("Win");
            gameManager.Win();
        }
    }
}
