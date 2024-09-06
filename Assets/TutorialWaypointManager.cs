using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialWaypointManager : MonoBehaviour
{
    [SerializeField] ParticleSystem particleFinal;
    [SerializeField] ParentTrigger[] triggers;
    [SerializeField] TextMeshProUGUI questText;

    private void Start()
    {
        triggers[0].PlayEffects();
    }
    public void PlayNext(ParentTrigger trigger)
    {
        for (int i = 0; i < 4; i++)
        {


            if (i == 3)
            {
                questText.text = "Save your child & escape !";
                particleFinal.Play();
                Destroy(triggers[i].gameObject);
                return;
            }
            else if (i == 2)
            {
                questText.text = "Tiger can eat your child !";
            }
            else if (i == 1)
            {
                questText.text = "Save your child!";
            }
            else if (i == 0)
            {
                questText.text = "Be aware Tiger !";
            }

            if (triggers[i]!= null && triggers[i] == trigger)
            {
                triggers[i+1].PlayEffects();
                Destroy(triggers[i].gameObject);
                return;
            }
        }
    }
}
