using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] float followSpeed = 10;
    [SerializeField] float ZOffset = 5.0f;

    Transform lookTarget;

    private void Awake()
    {
        lookTarget = FindObjectOfType<PlayerManager>().transform;
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(lookTarget.position.x,transform.position.y,lookTarget.position.z - ZOffset);
        transform.position = Vector3.Slerp(transform.position, targetPosition,Time.fixedDeltaTime * followSpeed);
    }
}
