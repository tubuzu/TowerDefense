// using System;
// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Managers;
// using TMPro;
// using UnityEngine.Events;

public class Options : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        //SFX slider
        sfxSlider.minValue = 0;
        sfxSlider.maxValue = 1;
        sfxSlider.value = Preferences.GetSFXVolume();
        sfxSlider.onValueChanged.AddListener(HandleSFXVolumeChange);

        //Music slider
        musicSlider.minValue = 0;
        musicSlider.maxValue = 1;
        musicSlider.value = Preferences.GetMusicVolume();
        musicSlider.onValueChanged.AddListener(HandleMusicVolumeChange);
    }

    private void HandleMusicVolumeChange(float volume)
    {
        Preferences.SetMusicVolume(volume);
        AudioManager.Instance.SetVolumeMusic(volume);
    }

    private void HandleSFXVolumeChange(float volume)
    {
        Preferences.SetSFXVolume(volume);
        AudioManager.Instance.SetVolumeSFX(volume);
    }
}