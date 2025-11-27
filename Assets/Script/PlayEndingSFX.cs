using UnityEngine;

public class PlayEndingSFX : MonoBehaviour
{
    void Start()
    {
        // Stop music safely
        if (AudioManager.Instance != null)
            AudioManager.Instance.StopMusic();

        int score = 0;

        // Only try to get score if ScoreManager exists
        if (ScoreManager.Instance != null)
        {
            score = ScoreManager.Instance.GetScore();
        }
        else
        {
            Debug.LogWarning("ScoreManager not found. Defaulting score to 0.");
        }

        // Play ending SFX based on score
        if (AudioManager.Instance != null)
        {
            if (score >= 250)
                AudioManager.Instance.PlaySFX("bestend");
            else if (score >= 100)
                AudioManager.Instance.PlaySFX("goodend");
            else
                AudioManager.Instance.PlaySFX("normalend");
        }
    }
}
