using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    public void DestroParent()
    {
        ParentTrigger parent = this.transform.parent.GetComponent<ParentTrigger>();
        parent.PlayNext();
    }
}
