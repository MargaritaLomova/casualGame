using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Controllers"), SerializeField]
    private AudioController audioController;
    [SerializeField]
    private CanvasController canvasController;
    [SerializeField]
    private ChunkController chunkController;
    [SerializeField]
    private AdvertisementController advertisementController;

    [Header("Objects From Scene"), SerializeField]
    private SphereController sphere;

    private int currentScore;

    private bool isGame;

    private Vector3 startSpherePosition;
    private Vector3 normalGravity;
    private Vector3 downGravity;

    private void Start()
    {
        normalGravity = Physics.gravity;
        downGravity = new Vector3(Physics.gravity.x, Physics.gravity.y * 1.8f, Physics.gravity.z);

        startSpherePosition = sphere.transform.position;

        Time.timeScale = 0;
    }

    #region Clicked

    public void OnVolumeChanged(bool value)
    {
        audioController.PlayButtonSound();

        if (value)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

    public void OnPlayClicked()
    {
        audioController.PlayButtonSound();

        LoadLevel();
    }

    public void OnRestartClicked()
    {
        audioController.PlayButtonSound();

        LoadLevel();
    }

    public void OnPauseClicked()
    {
        audioController.PlayButtonSound();

        isGame = false;

        canvasController.ShowPause();

        Time.timeScale = 0;
    }

    public void OnClosePauseClicked()
    {
        audioController.PlayButtonSound();

        canvasController.ShowLevelUI();

        isGame = true;
        StartCoroutine(CalculateScore());
        StartCoroutine(CalculateBoost());
        StartCoroutine(ChangeGravity());

        Time.timeScale = 1;
    }

    public void OnBackToMenuClicked()
    {
        audioController.PlayButtonSound();

        canvasController.ShowMenu();

        isGame = false;

        Time.timeScale = 0;
    }

    public void OnExitClicked()
    {
        audioController.PlayButtonSound();

        Application.Quit();
    }

    #endregion

    public void Death()
    {
        advertisementController.PlusAddCount();

        isGame = false;

        StartCoroutine(ReloadLevelByDeath());
    }

    public int GetBoost()
    {
        return currentScore / 1000;
    }

    public bool IsGame()
    {
        return isGame;
    }

    private void LoadLevel()
    {
        canvasController.ShowLevelUI();

        isGame = true;
        StartCoroutine(CalculateScore());
        StartCoroutine(CalculateBoost());
        StartCoroutine(ChangeGravity());

        currentScore = 0;
        canvasController.UpdateGameScore(currentScore);

        Time.timeScale = 1;
    }

    private IEnumerator ReloadLevelByDeath()
    {
        sphere.transform.position = startSpherePosition;
        yield return new WaitForSeconds(1);
        chunkController.RemoveSpawnedChuncks();

        LoadLevel();
    }

    private IEnumerator ChangeGravity()
    {
        while(isGame)
        {
            if (sphere.IsStay())
            {
                Physics.gravity = normalGravity;
            }
            else
            {
                Physics.gravity = downGravity;
            }

            yield return null;
        }
    }

    private IEnumerator CalculateScore()
    {
        while (isGame)
        {
            yield return new WaitForSeconds(1);

            currentScore++;
            canvasController.UpdateGameScore(currentScore);
        }
    }

    private IEnumerator CalculateBoost()
    {
        while (isGame)
        {
            var sphereBoost = currentScore / 1000;

            sphere.Move((Vector3.right * Time.deltaTime * ((int)sphereBoost > 1 ? (int)sphereBoost : 1)) / 3);

            yield return null;
        }
    }
}