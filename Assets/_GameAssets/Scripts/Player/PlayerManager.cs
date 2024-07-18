using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof( LocomotionManager), typeof(AnimatorManager))]
public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    LocomotionManager locomotionManager;
    AnimatorManager animatorManager;
    GameManager gameManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        locomotionManager = GetComponent<LocomotionManager>();
        animatorManager = GetComponent<AnimatorManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        inputManager.InputHandler();
    }

    private void FixedUpdate()
    {
        //! Move Player
        locomotionManager.LocomotionHandler();
        //! Animate player
        animatorManager.AnimatorHandler();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Strings.CoinTag))
        {
            //TODO Add SFX for coin
            //! collect Coin
            gameManager.Coins++;
            Destroy(other.gameObject);
        }
    }
}
