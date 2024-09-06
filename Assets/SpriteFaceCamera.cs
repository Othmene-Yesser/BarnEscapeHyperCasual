using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFaceCamera : MonoBehaviour
{
    [SerializeField] Camera _camera;
    private void LateUpdate()
    {
        Vector3 direction = _camera.transform.position - transform.position;
        direction.Normalize();
        transform.forward = direction;
    }
}
