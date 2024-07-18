using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 XZInput;

    FloatingJoystick mobileInput;

    private void Awake()
    {
        mobileInput = FindObjectOfType<FloatingJoystick>();
    }

    public void InputHandler()
    {
        HandleLocomotionInput();
    }

    private void HandleLocomotionInput()
    {
        XZInput.x = mobileInput.Vertical;
        XZInput.y = mobileInput.Horizontal;
    }
}
