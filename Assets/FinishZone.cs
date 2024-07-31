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
            gameManager.Win();
        }
    }
}
