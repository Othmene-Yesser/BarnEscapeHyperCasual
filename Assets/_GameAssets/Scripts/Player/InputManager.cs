using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 XZInput;

    public void InputHandler()
    {
        HandleLocomotionInput();
    }

    //TODO Change so that it reads input from a joystick like in mobile games
    private void HandleLocomotionInput()
    {
        XZInput.x = Input.GetAxisRaw("Vertical");
        XZInput.y = Input.GetAxisRaw("Horizontal");
    }
}
