using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject panelPause, panelGameOver;

    [SerializeField]
    Button menuButton, reloadGameButton, continueButton, pauseButton;

    [SerializeField]
    Text hunterStatus;

    private bool pauseNow;

    internal static Action<bool> OnPauseNow;

    private void Start()
    {
        menuButton.onClick.AddListener(OpenMenu);
        reloadGameButton.onClick.AddListener(ReloadGame);
        continueButton.onClick.AddListener(PauseDeactivated);
        pauseButton.onClick.AddListener(PauseActivated);
        PlayerControls.OnPlayerHunter += StatusPlayer;
        PlayerControls.OnGameOver += ChecGameProcess;
        pauseNow = false;
    }

    private void ChecGameProcess(bool gameOver)
    {
        if (gameOver)
            GameOver();
    }

    private void OnDestroy()
    {
        PlayerControls.OnPlayerHunter -= StatusPlayer;
        PlayerControls.OnGameOver -= ChecGameProcess;
    }

    private void StatusPlayer(bool hunter)
    {
        if (hunter)
            hunterStatus.gameObject.SetActive(true);
        else
            hunterStatus.gameObject.SetActive(false);
    }

    private void ReloadGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OpenMenu()
    {
        SceneManager.LoadScene(0);
    }

    internal void PauseActivated()
    {
        pauseNow = true;
        OnPauseNow?.Invoke(true);
        panelPause.SetActive(true);
    }

    internal void PauseDeactivated()
    {
        pauseNow = false;
        OnPauseNow?.Invoke(false);
        panelPause.SetActive(false);
    }

    internal async void GameOver()
    {
        panelGameOver.SetActive(true);
        await Task.Delay(2000);
        OpenMenu();
    }
}
