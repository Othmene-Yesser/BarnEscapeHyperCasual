using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] GameObject padLock;
    [SerializeField] GameObject stars;

    public bool locked;
    public int levelNumber;

    public void AssignValuesForLevel(int lvlNbr)
    {
        levelNumber = lvlNbr;

        if (!PlayerPrefs.HasKey("StartedGame"))
        {
            PlayerPrefs.SetInt(StringsAndConsts.LevelReached, 0);
            PlayerPrefs.SetInt("StartedGame", 1);
        }
        locked = !(PlayerPrefs.GetInt(StringsAndConsts.LevelReached) >= levelNumber);
        if (locked || IsHardLevel())
        {
            //! Display the Lock
            GameObject lockOverlay = Instantiate(padLock, transform);
            lockOverlay.transform.position = transform.position;
        }
        else
        {
            //! Display the stars
            if ( levelNumber < PlayerPrefs.GetInt(StringsAndConsts.LevelReached) )
            {
                //! Display the stars collected in this level
                var starDisplay = Instantiate(stars, transform);
                StarsLevel levelStarDisplay = starDisplay.GetComponent<StarsLevel>();
                levelStarDisplay.transform.position = transform.position;
                levelStarDisplay.ShowStars( PlayerPrefs.GetInt(levelNumber.ToString()) );
            }
            else if (PlayerPrefs.GetInt(StringsAndConsts.LevelReached) == levelNumber)
            {
                //! Display empty stars for the unlocked level which has not been cleared yet
                var starDisplay = Instantiate(stars, transform);
                StarsLevel levelStarDisplay = starDisplay.GetComponent<StarsLevel>();
                levelStarDisplay.transform.position = transform.position;
                levelStarDisplay.ShowStars(0);
            }
        }

    }

    private bool IsHardLevel() 
    {
        int levelNumber = this.levelNumber;
        levelNumber++;
        if (levelNumber % 12 == 0)
        {
            int categoryNumber = levelNumber / 12;
            int valueToUnlockLevel = StringsAndConsts.MinStarsCollected * categoryNumber;
            if (PlayerPrefs.GetInt(StringsAndConsts.StarsCollected) >= valueToUnlockLevel)
            {
                locked = false;
                return false;
            }
            else
            {
                locked = true;
                return true;
            }
        }
        return locked;
    }

    public void PlayLevel()
    {
        if (locked)
        {
            Debug.Log("Locked");
            return;
        }
        SceneManager.LoadScene(levelNumber + 1);
    }
    
}
