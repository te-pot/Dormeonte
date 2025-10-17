using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour
{
    [Header("Fade Settings")]
    public Image fadeImage;
    public float fadeDuration = 1f;

    public void StartGameMenu()
    {
        StartCoroutine(FadeAndLoadScene("StartMenu"));
    }

    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Trigger music fade if AudioManager exists
        if (AudioManager.Instance != null)
            StartCoroutine(AudioManager.Instance.FadeOutMusic(fadeDuration));

        // Start fade to black
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            Color color = fadeImage.color;
            color.a = 0;
            fadeImage.color = color;

            float t = 0;
            while (t < fadeDuration)
            {
                t += Time.unscaledDeltaTime;
                color.a = Mathf.Lerp(0, 1, t / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }
        }

        SceneManager.LoadScene(sceneName);
    }
}
