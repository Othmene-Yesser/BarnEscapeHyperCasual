using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringsAndConsts
{
    public static string BlendTree1D = "Blend";
    public static string PlayerTag = "Player";
    public static string SeekerTag = "Seeker";
    public static string AllyTag = "Ally";
    public static string CoinTag = "Coin";
    public static string ZaWarudoBuff = "TimerStop";
    public static string SpeedBuff = "SpeedBuff";
    public static string Invisibility = "Invisibility";
    public static string Score = "Score";
    public static string LevelReached = "LevelReached";
    public static string SelectedSkin = "Selectedskin";
    public static string StarsCollected = "Stars";
    public const int MinStarsCollected = 28;
}

public static class CoinManagerFunctions
{
    public static void StoreCoins(int coins)
    {
        PlayerPrefs.SetInt(StringsAndConsts.CoinTag, coins);
    }
}
