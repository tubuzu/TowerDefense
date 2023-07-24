using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Managers;
using Scenes;

public class LevelButton : MonoBehaviour
{
    public bool isActive;
    private Image buttonImage;
    private Button myButton;
    private int starsActive;
    [SerializeField] private Sprite starActive;
    [SerializeField] private Sprite starInactive;

    public Image[] stars;
    public TextMeshProUGUI levelText;
    [SerializeField] Color inactiveColor;
    public int level;

    public void Setup(int level)
    {
        buttonImage = GetComponent<Image>();
        myButton = GetComponent<Button>();

        this.level = level;

        //Decide if the level is active
        if (GameData.instance.saveData.isActive[level])
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

        //Decide how many stars to activate
        starsActive = GameData.instance.saveData.stars[level];

        for (int i = 0; i < 3; i++)
        {
            if (i < starsActive)
                stars[i].sprite = starActive;
            else
                stars[i].sprite = starInactive;
        }

        levelText.text = "" + (level + 1);

        if (isActive)
        {
            Color color = buttonImage.color;
            color.a = 1f;
            buttonImage.color = color;
            myButton.enabled = true;
            levelText.enabled = true;
        }
        else
        {
            Color color = buttonImage.color;
            color.a = 0.5f;
            buttonImage.color = color;
            myButton.enabled = false;
            levelText.color = inactiveColor;
        }
    }

    public void SelectThisLevel()
    {
        AudioManager.Instance.PlayButtonClickSfx();
        LevelLoader.Instance.LoadGameScene(level + 1);
    }
}
