using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preferences : MonoBehaviour
{

    private const string SFX = "sfx";
    private const string MUSIC = "music";
    private const string CURRENT_LVL = "currLvl";
    private const string MAX_LVL = "maxLvl";

    public static int GetCurrentLvl() => PlayerPrefs.GetInt(CURRENT_LVL, 1);

    public static void SetCurrentLvl(int lvl) => PlayerPrefs.SetInt(CURRENT_LVL, lvl);
    public static int GetMaxLvl() => PlayerPrefs.GetInt(MAX_LVL, 1);

    public static void SetMaxLvl(int lvl) => PlayerPrefs.SetInt(MAX_LVL, Mathf.Max(PlayerPrefs.GetInt(MAX_LVL, 1), lvl));

    public static void SetSFXVolume(float volume) => PlayerPrefs.SetFloat(SFX, volume);

    public static float GetSFXVolume() => PlayerPrefs.GetFloat(SFX, 1);

    public static void SetMusicVolume(float volume) => PlayerPrefs.SetFloat(MUSIC, volume);

    public static float GetMusicVolume() => PlayerPrefs.GetFloat(MUSIC, 1);
}