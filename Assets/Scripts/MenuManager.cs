using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Button gameButton, quitButton;

    private void Start()
    {
        gameButton.onClick.AddListener(PlayGame);
        quitButton.onClick.AddListener(QuitApp);
    }

    private void QuitApp()
    {
        Application.Quit();
    }

    private void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
}
