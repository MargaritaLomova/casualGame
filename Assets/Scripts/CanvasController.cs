using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [Header("Blocks"), SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject levelUI;
    [SerializeField]
    private GameObject pause;
    [SerializeField]
    private GameObject death;

    [Header("Texts"), SerializeField]
    private TMP_Text gameScore;
    [SerializeField]
    private TMP_Text highScore;

    private void Start()
    {
        highScore.text = $"{PlayerPrefs.GetInt("HighScore")}";

        ShowMenu();
    }

    public void ShowMenu()
    {
        menu.SetActive(true);

        levelUI.SetActive(false);
        pause.SetActive(false);
        death.SetActive(false);
    }

    public void ShowLevelUI()
    {
        levelUI.SetActive(true);

        menu.SetActive(false);
        pause.SetActive(false);
        death.SetActive(false);
    }

    public void ShowPause()
    {
        levelUI.SetActive(true);
        pause.SetActive(true);

        menu.SetActive(false);
        death.SetActive(false);
    }

    public void ShowDeath()
    {
        levelUI.SetActive(true);
        death.SetActive(true);

        menu.SetActive(false);
        pause.SetActive(false);
    }

    public void UpdateGameScore(int currentScore)
    {
        gameScore.text = $"{currentScore}";
        if (currentScore > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            highScore.text = $"{currentScore}";
        }
    }
}