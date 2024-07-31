using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    float rotationSpeed;

    private void Start()
    {
        rotationSpeed = 2.5f;
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, rotationSpeed, 0));
    }
}
