using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager), typeof( LocomotionManager), typeof(AnimatorManager))]
public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    LocomotionManager locomotionManager;
    AnimatorManager animatorManager;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        locomotionManager = GetComponent<LocomotionManager>();
        animatorManager = GetComponent<AnimatorManager>();
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
}
