using UnityEngine;
using UnityEngine.SceneManagement; // Required for SceneManager

public class MainManuController : MonoBehaviour
{
    public void StartGame()
    {
        Time.timeScale = 1; // Reset time scale
        SceneManager.LoadScene("GamePlay");

    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("Settings");

    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");

    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("StartMenu");
    }
}