using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTrigger : MonoBehaviour
{
    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
}
