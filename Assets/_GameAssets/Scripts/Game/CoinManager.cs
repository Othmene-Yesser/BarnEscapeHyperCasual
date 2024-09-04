using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public int coins;

    TextMeshProUGUI coinText;

    private void Awake()
    {
        coinText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(StringsAndConsts.CoinTag))
        {
            coins = PlayerPrefs.GetInt(StringsAndConsts.CoinTag);
        }
        else
        {
            PlayerPrefs.SetInt(StringsAndConsts.CoinTag, 0);
            coins = 0;
        }
        coinText.text = coins.ToString();
    }

    public void UpdateCoinValue()
    {
        coins = PlayerPrefs.GetInt(StringsAndConsts.CoinTag);
        coinText.text = coins.ToString();
    }

}
