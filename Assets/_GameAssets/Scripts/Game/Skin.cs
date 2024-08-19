using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    SkinSelectMenu menu;

    public RawImage skinImage;
    Color normalColor;

    string skinName;
    bool locked;

    private void Awake()
    {
        menu = transform.parent.GetComponent<SkinSelectMenu>();
        skinImage = GetComponent<RawImage>();
    }

    private void Start()
    {
        skinName = "Skin" + menu.GetSkinNumber(this).ToString();
        locked = PlayerPrefs.GetInt(skinName) == 0;
        normalColor = skinImage.color;
        if (locked)
        {
            skinImage.color = Color.gray;
        }
    }

    private bool UnlockSkin()
    {
        var coins = PlayerPrefs.GetInt(Strings.CoinTag);
        var cost = 5 * menu.GetSkinNumber(this);
        if (coins >= cost)
        {
            PlayerPrefs.SetInt(Strings.CoinTag, coins - cost);
            //! remove the lock
            PlayerPrefs.SetInt(skinName, 1);
            locked = PlayerPrefs.GetInt(skinName) == 0;
            //! Update coins
            CoinManager coinManager = FindObjectOfType<CoinManager>();
            coinManager.UpdateCoinValue();
            skinImage.color = normalColor;
            return true;
        }
        else
        {
            Debug.Log("Insufficient funds");
            return false;
        }
    }
    public void SelectSkin()
    {
        if (locked)
        {
            var unlocked = UnlockSkin();
            if (!unlocked)
            {
                return;
            }
        }
        //! then select if the skin is unlocked
        PlayerPrefs.SetInt(Strings.SelectedSkin,menu.GetSkinNumber(this));
        menu.selectedSkin.color = skinImage.color;
    }
}
