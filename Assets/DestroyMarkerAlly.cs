using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMarkerAlly : MonoBehaviour
{
    [SerializeField] GameObject marker;
    Collider colliderAlly;
    private void Awake()
    {
        colliderAlly = GetComponent<Collider>();
    }
    private void FixedUpdate()
    {
        if (colliderAlly.enabled)
        {
            //! Destroy the marker
            Destroy(marker);
            Destroy(this);
        }
    }
}
