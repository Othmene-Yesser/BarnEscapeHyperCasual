using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [SerializeField] GameObject[] Skins;

    private void Awake()
    {
        int index = PlayerPrefs.GetInt(StringsAndConsts.SelectedSkin);
        Instantiate(Skins[index],transform);
        Skins = null;
    }
}
