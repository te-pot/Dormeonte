using UnityEngine;

public class EndingSceneSetup : MonoBehaviour
{
    [Header("Ending Music & SFX")]
    public AudioClip endingMusic; // optional
    public AudioClip sfxHigh;
    public AudioClip sfxMedium;
    public AudioClip sfxLow;

    void Start()
    {
        Debug.Log("EndingSceneSetup Start called!");

        // Stop previous background music
        AudioManager.Instance.StopMusic();

        // Play ending music if assigned
        if (endingMusic != null)
        {
            AudioManager.Instance.PlayMusic(endingMusic);
        }

        // Play SFX based on score
        int score = ScoreManager.Instance != null ? ScoreManager.Instance.GetScore() : 0;

        if (score >= 120 && sfxHigh != null)
        {
            Debug.Log("Playing High Score SFX");
            AudioManager.Instance.PlaySFX(sfxHigh);
        }
        else if (score >= 100 && sfxMedium != null)
        {
            Debug.Log("Playing Medium Score SFX");
            AudioManager.Instance.PlaySFX(sfxMedium);
        }
        else if (sfxLow != null)
        {
            Debug.Log("Playing Low Score SFX");
            AudioManager.Instance.PlaySFX(sfxLow);
        }


    }




}
