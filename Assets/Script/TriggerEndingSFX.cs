using UnityEngine;

public class TriggerEndingSFX : MonoBehaviour
{
    void Start()
    {
        /*var audioManager = Object.FindFirstObjectByType<EndingSceneAudioManager>();
        if (audioManager == null)
        {
            Debug.LogWarning("No EndingSceneAudioManager found!");
            return;
        }
        */

        int score = ScoreManager.Instance != null ? ScoreManager.Instance.GetScore() : 0;

        if (score >= 120)
            AudioManager.Instance.PlaySFX("bestend"); // high score
        else if (score >= 100)
            AudioManager.Instance.PlaySFX("goodend"); // medium score
        else
            AudioManager.Instance.PlaySFX("normalend"); // low score
    }
}
