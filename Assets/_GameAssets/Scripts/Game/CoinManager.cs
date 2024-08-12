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
        if (PlayerPrefs.HasKey(Strings.CoinTag))
        {
            coins = PlayerPrefs.GetInt(Strings.CoinTag);
        }
        else
        {
            PlayerPrefs.SetInt(Strings.CoinTag, 0);
            coins = 0;
        }
        coinText.text = coins.ToString();
    }

    public void UpdateCoinValue()
    {
        coins = PlayerPrefs.GetInt(Strings.CoinTag);
        coinText.text = coins.ToString();
    }

}
