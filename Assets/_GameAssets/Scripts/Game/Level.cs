using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public bool locked;
    public int levelNumber;

    public void AssignValuesForLevel(int lvlNbr)
    {
        levelNumber = lvlNbr;

        if (!PlayerPrefs.HasKey("StartedGame"))
        {
            PlayerPrefs.SetInt(Strings.LevelReached, 0);
            PlayerPrefs.SetInt("StartedGame", 1);
        }
        locked = !(PlayerPrefs.GetInt(Strings.LevelReached) >= levelNumber);
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
