using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectMenu : MonoBehaviour
{
    public RawImage selectedSkin;
    [SerializeField] Skin[] skins;

    private void Awake()
    {
        skins = new Skin[transform.childCount];
        //! Go through every skin and assign if its locked or not 
        if (PlayerPrefs.HasKey("StartedGameSkins"))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                skins[i] = transform.GetChild(i).GetComponent<Skin>();
            }
        }
        else
        {
            //! First skin is unlocked
            skins[0] = transform.GetChild(0).GetComponent<Skin>();
            var skinName = "Skin0";
            PlayerPrefs.SetInt(skinName, 1);
            
            for (int i = 1; i < transform.childCount; i++)
            {
                skins[i] = transform.GetChild(i).GetComponent<Skin>();
                skinName = "Skin" + i.ToString();
                PlayerPrefs.SetInt(skinName,0);
            }
            PlayerPrefs.SetInt(StringsAndConsts.SelectedSkin, 0);
            PlayerPrefs.SetInt("StartedGameSkins", 1);
        }
    }
    private void Start()
    {
        if (PlayerPrefs.HasKey(StringsAndConsts.SelectedSkin))
        {
            selectedSkin.texture = skins[PlayerPrefs.GetInt(StringsAndConsts.SelectedSkin)].skinImage.texture;
        }
    }

    public int GetSkinNumber(Skin skin)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (skins[i] == skin)
                return i;
        }
        Debug.LogWarning("SkinInvalid Somehow");
        return -1;
    }
}
