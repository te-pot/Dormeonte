using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(PlaySFXAndLoadScene(AudioManager.Instance.sfx1, "GamePlay"));
    }

    public void OpenCredits()
    {
        StartCoroutine(PlaySFXAndLoadScene(AudioManager.Instance.sfx2, "Credits"));
    }

    public void ReturntoHome()
    {
        StartCoroutine(PlaySFXAndLoadScene(AudioManager.Instance.sfx1, "StartMenu"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator PlaySFXAndLoadScene(AudioClip clip, string sceneName)
    {
        if (clip != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.sfxSource.clip = clip;
            AudioManager.Instance.sfxSource.Play();

            // Wait for the sound to finish
            yield return new WaitForSeconds(clip.length);
        }

        Time.timeScale = 1; // Just in case it was paused
        SceneManager.LoadScene(sceneName);
    }
}
