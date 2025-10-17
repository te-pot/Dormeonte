using UnityEngine;

public class TriggerEndingSFX : MonoBehaviour
{
    void Start()
    {
        var audioManager = FindObjectOfType<EndingSceneAudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("No EndingSceneAudioManager found!");
            return;
        }

        int score = ScoreManager.Instance != null ? ScoreManager.Instance.GetScore() : 0;

        if (score >= 120)
            audioManager.PlaySFX(1); // high score
        else if (score >= 100)
            audioManager.PlaySFX(2); // medium score
        else
            audioManager.PlaySFX(3); // low score
    }
}
