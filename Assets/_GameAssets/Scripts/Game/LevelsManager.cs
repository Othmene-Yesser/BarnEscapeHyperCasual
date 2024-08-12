using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    public Level[] levels;

    private void Awake()
    {
        levels = new Level[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            levels[i] = transform.GetChild(i).GetComponent<Level>();
        }
    }

    public int GetLevelNumber(Level level)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (levels[i] == level)
            {
                return i;
            }
        }
        return -1;
        
    }

    public void ClearAllKeysPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
