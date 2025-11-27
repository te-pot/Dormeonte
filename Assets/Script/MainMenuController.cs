using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetScore();

        StartCoroutine(PlaySFXAndLoadScene("meow1", "GamePlay"));
    }

    public void OpenCredits()
    {
        StartCoroutine(PlaySFXAndLoadScene("meow2", "Credits"));
    }

    public void ReturntoHome()
    {
        StartCoroutine(PlaySFXAndLoadScene("meow1", "StartMenu"));
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator PlaySFXAndLoadScene(string sfxName, string sceneName)
    {
        if (AudioManager.Instance != null && AudioManager.Instance.sfxSource != null)
        {
            // Find the sound clip manually
            Sound s = System.Array.Find(AudioManager.Instance.sfxSounds, x => x.name == sfxName);
            if (s != null)
            {
                AudioManager.Instance.sfxSource.PlayOneShot(s.clip);
                yield return new WaitForSeconds(s.clip.length);
            }
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

}
