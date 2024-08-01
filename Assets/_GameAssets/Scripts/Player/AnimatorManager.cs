using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    InputManager inputManager;
    Animator animator;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animator = transform.GetChild(0).GetComponent<Animator>();
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
    public void PlayTargetAnimation(string animation = "")
    {
        if (animation == "")
            return;
        animator.Play(animation);
    }
}
