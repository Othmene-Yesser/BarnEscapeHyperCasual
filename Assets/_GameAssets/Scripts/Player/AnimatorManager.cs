using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorManager : MonoBehaviour
{
    InputManager inputManager;
    Animator animator;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = GetComponent<Animator>();
    }

    public void AnimatorHandler()
    {
        HandleMovementAnimation();
    }
    private void HandleMovementAnimation()
    {
        float magnitude = inputManager.XZInput.normalized.magnitude;
        animator.SetFloat(Strings.BlendTree1D, magnitude);

    }
}
