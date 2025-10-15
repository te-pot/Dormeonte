using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuTransition : MonoBehaviour
{
    [SerializeField] GameObject EndMenu;

    public void Home()
    {
        Time.timeScale = 1f; // ✅ Unpause before switching scenes
        SceneManager.LoadScene("StartMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        if (ScoreManager.Instance != null)
            Destroy(ScoreManager.Instance.gameObject); // 👈 remove old one

        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
