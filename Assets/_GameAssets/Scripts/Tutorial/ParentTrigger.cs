using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentTrigger : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particleSystems;
    TutorialWaypointManager tutoWayMan;

    private void Awake()
    {
        tutoWayMan = transform.parent.GetComponent<TutorialWaypointManager>();
        for (int i = 0; i < 3; i++)
        {
            particleSystems[i].Stop();
        }
    }

    public void PlayEffects()
    {
        for (int i = 0; i < 3; i++)
        {
            particleSystems[i].Play();
        }
    }

    public void PlayNext()
    {
        tutoWayMan.PlayNext(this);
    }
}
