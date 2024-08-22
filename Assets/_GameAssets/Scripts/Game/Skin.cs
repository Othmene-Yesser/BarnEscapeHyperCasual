using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skin : MonoBehaviour
{
    [SerializeField] GameObject padLock;

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
            //! Get the lock an then display it with little transperacy 
            GameObject pad = Instantiate(padLock, transform);
            pad.transform.position = transform.position;
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
            Destroy(transform.GetChild(0).gameObject);
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
        menu.selectedSkin.texture = skinImage.texture;
    }
}
