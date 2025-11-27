using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuTransition : MonoBehaviour
{
    [SerializeField] GameObject EndMenu;

    public void Home()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        if (ScoreManager.Instance != null)
            Destroy(ScoreManager.Instance.gameObject);

        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
