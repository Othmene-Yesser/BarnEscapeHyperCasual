using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LocomotionManager : MonoBehaviour
{
    [SerializeField] PlayerDataScriptableObject playerData;

    Rigidbody rb;

    Transform cameraObject;

    InputManager inputManager;

    private void Awake()
    {
        inputManager= GetComponent<InputManager>();
        cameraObject = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    public void LocomotionHandler()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        Vector3 moveDirection;
        moveDirection = cameraObject.forward * inputManager.XZInput.x;
        moveDirection += cameraObject.right * inputManager.XZInput.y;
        moveDirection.Normalize();
        moveDirection.y = 0;

        moveDirection *= playerData.speed;

        rb.velocity = moveDirection;
    }
    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.XZInput.x;
        targetDirection += cameraObject.right * inputManager.XZInput.y;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, playerData.rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }
}
