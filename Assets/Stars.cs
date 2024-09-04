using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stars : MonoBehaviour
{
    TextMeshProUGUI amount;

    private void OnEnable()
    {
        if (amount == null)
        {
            amount = GetComponent<TextMeshProUGUI>();
        }
        amount.text = PlayerPrefs.GetInt(StringsAndConsts.StarsCollected).ToString();
    }
}
