using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            var levelReached = PlayerPrefs.GetInt(Strings.LevelReached);
            PlayerPrefs.SetInt(Strings.LevelReached, levelReached + 1);

            Debug.Log("Win");
            gameManager.Win();
        }
    }
}
