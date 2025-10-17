using UnityEngine;

public class PlayEndingSFX : MonoBehaviour
{
    public AudioManager audioManager;

    void Start()
    {
        int score = ScoreManager.Instance.GetScore();

        AudioManager.Instance.StopMusic();

        if (score >= 120)
            AudioManager.Instance.PlaySFX1();
        else if (score >= 100)
            AudioManager.Instance.PlaySFX2();
        else if (score >= 0)
            AudioManager.Instance.PlaySFX3();
    }
}
