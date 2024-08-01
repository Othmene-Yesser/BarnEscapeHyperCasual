using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotationAnimation : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] bool overrideRotaion;

    private void Start()
    {
        float aux = rotationSpeed;
        rotationSpeed = 2.5f;
        if (overrideRotaion)
        {
            rotationSpeed = aux;
        }
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationSpeed, 0));
    }
}
