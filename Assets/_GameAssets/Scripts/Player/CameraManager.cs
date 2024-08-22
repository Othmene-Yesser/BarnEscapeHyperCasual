using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    private void Awake()
    {
        Transform player = FindObjectOfType<PlayerManager>().transform;
        CinemachineVirtualCamera virtualcamera = transform.GetChild(0).GetComponent<CinemachineVirtualCamera>();
        virtualcamera.Follow = player;
        virtualcamera.LookAt = player;
    }
}
