using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof(LocomotionManager), typeof(AnimatorManager))]
public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    LocomotionManager locomotionManager;
    [HideInInspector] public AnimatorManager animatorManager;

    GameManager gameManager;
    AudioManager audioManager;

    public bool isAlive;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        locomotionManager = GetComponent<LocomotionManager>();
        animatorManager = GetComponent<AnimatorManager>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        isAlive = true;
    }

    private void Update()
    {
        if (!isAlive)
        {
            inputManager.XZInput = Vector2.zero;
            return;
        }
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
        if (other.CompareTag(StringsAndConsts.CoinTag))
        {
            //TODO Add SFX for coin
            audioManager.PlaySoundEffect(audioManager.collectCoin);
            //! collect Coin
            gameManager.Coins++;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Tutorial"))
        {
            //! Update Tutorial UI
            Tutorial tuto = FindObjectOfType<Tutorial>();
            TriggerTutorial triggerTuto = other.GetComponent<TriggerTutorial>();
            triggerTuto.DestroParent();
            //tuto.UpdateUI();
        }
    }
}
