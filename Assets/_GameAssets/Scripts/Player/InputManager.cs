using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 XZInput;
    public bool immobile = false;

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
        if (immobile)
        {
            XZInput = Vector2.zero;
            return;
        }
        XZInput.x = mobileInput.Vertical;
        XZInput.y = mobileInput.Horizontal;
    }
}
