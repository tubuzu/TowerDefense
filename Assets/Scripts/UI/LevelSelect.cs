// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Managers;

public class LevelSelect : MonoBehaviour
{
    public static LevelSelect instance;

    public GameObject levelButton;
    public Transform levelGrid;

    private int currentPage = 0;
    private int levelPerPage = 9;
    private LevelButton[] buttons;

    public GameObject nextButton;
    public GameObject prevButton;

    private int GetTotalPage() => Mathf.CeilToInt(GameData.instance.totalLevel / levelPerPage);

    private void Awake()
    {
        LevelSelect.instance = this;
    }

    private void Start()
    {
        SetupButtons();
    }

    private void SetupButtons()
    {
        int totalPage = GetTotalPage();
        nextButton.SetActive(totalPage > 1);
        prevButton.SetActive(false);

        buttons = new LevelButton[levelPerPage];
        for (int i = 0; i < Mathf.Min(GameData.instance.totalLevel - currentPage * levelPerPage, levelPerPage); i++)
        {
            GameObject button = Instantiate(levelButton, levelGrid.position, Quaternion.identity);
            button.transform.SetParent(levelGrid);
            button.transform.localScale = Vector3.one;
            button.GetComponent<LevelButton>().Setup(i);
            buttons[i] = button.GetComponent<LevelButton>();
        }
    }

    private void OnEnable()
    {
        currentPage = 0;
    }

    public void NextPage()
    {
        int totalPage = GetTotalPage();
        if (currentPage < totalPage - 1)
        {
            currentPage++;
            SetupPage();
        }
        nextButton.SetActive(currentPage < totalPage - 1);
        prevButton.SetActive(currentPage > 0);
        AudioManager.Instance.PlayButtonClickSfx();
    }

    public void PrevPage()
    {
        int totalPage = GetTotalPage();
        if (currentPage > 0)
        {
            currentPage--;
            SetupPage();
        }
        nextButton.SetActive(currentPage < totalPage - 1);
        prevButton.SetActive(currentPage > 0);
        AudioManager.Instance.PlayButtonClickSfx();
    }

    private void SetupPage()
    {
        for (int i = 0; i < Mathf.Min(GameData.instance.totalLevel - currentPage * levelPerPage, levelPerPage); i++)
        {
            buttons[i].Setup(currentPage * levelPerPage + i);
        }
    }
}
