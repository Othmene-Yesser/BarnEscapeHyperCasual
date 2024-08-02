using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tutoText;

    GameManager gameManager;

    int i = 0;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        i = 0;
    }

    public void UpdateUI()
    {
        Debug.Log("Test");
        if (i == 0)
        {
            //! Display Buffs
            tutoText.text = "There are seekers which their objective is to capture you navigate around them using Buffs";
        }
        else if (i == 1)
        {
            //! Display Allies collection
            tutoText.text = "There are allies 'Pigeon' which you need to collect them all to finish the level. \nThere are also coins which you can collect";
        }
        else if (i == 2)
        {
            //! Display Allies can be killed
            tutoText.text = "Be carefull your allies can be captured if they are seen by the seekers";
        }
        else if (i == 3)
        {
            //! Display that you have to collect all allies for the door to be opened
            tutoText.text = "Once you collect all the allies a door will open then you'll have to exit through it to win the level";
        }
        i++;
    }
    private void Update()
    {
        if (gameManager.winZone.enabled == false)
        {
            Destroy(gameObject);
        }
    }
}
