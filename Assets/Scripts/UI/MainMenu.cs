// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Managers;

public class MainMenu : MonoBehaviour
{
    private enum Sub
    {
        Main,
        LevelSelection,
        Options,
    }

    GameObject mainSub;
    GameObject optionSub;
    GameObject levelSelectSub;

    private void Awake()
    {
        mainSub = transform.Find("MainMenu").gameObject;
        optionSub = transform.Find("Options").gameObject;
        levelSelectSub = transform.Find("SelectLevel").gameObject;
        mainSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        optionSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        levelSelectSub.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }
    private void Start()
    {
        // Play music
        // musicPlayer..PlayMusic(MusicSound.BackgroundSound);

        // Display main menu
        ShowSub(Sub.Main);
    }
    private void ShowSub(Sub sub)
    {
        mainSub.SetActive(false);
        optionSub.SetActive(false);
        levelSelectSub.SetActive(false);

        switch (sub)
        {
            case Sub.Main:
                mainSub.SetActive(true);
                break;
            case Sub.Options:
                optionSub.SetActive(true);
                break;
            case Sub.LevelSelection:
                levelSelectSub.SetActive(true);
                break;
        }
    }
    public void Play()
    {
        AudioManager.Instance.PlayButtonClickSfx();
        ShowSub(Sub.LevelSelection);
    }
    public void Option()
    {
        AudioManager.Instance.PlayButtonClickSfx();
        ShowSub(Sub.Options);
    }
    public void Back()
    {
        AudioManager.Instance.PlayButtonClickSfx();
        ShowSub(Sub.Main);
    }
    public void Quit()
    {
        AudioManager.Instance.PlayButtonClickSfx();
        Application.Quit();
    }
}
