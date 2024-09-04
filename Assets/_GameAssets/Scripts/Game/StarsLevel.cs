using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsLevel : MonoBehaviour
{
    public void ShowStars(int stars)
    {
        if (stars == 0)
        {
            //! Do nothing
            return;
        }

        //! Get the stars within the game object
        RawImage[] starsImages = new RawImage[3];
        for (int i = 0; i < transform.childCount; i++)
        {
            starsImages[i] = transform.GetChild(i).GetComponent<RawImage>();
        }

        if (stars >= 1)
        {
            starsImages[0].color = Color.white;
            if (stars >= 2)
            {
                starsImages[1].color = Color.white;
                if (stars >= 3)
                {
                    starsImages[2].color = Color.white;

                    //! Just to make sure see if its bigger than three then we have a problem
                    if (stars > 3)
                    {
                        Debug.LogError("StarLevel Problem stars were greater than 3");
                    }
                }
            }
        }
    }
}
