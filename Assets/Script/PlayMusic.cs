using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    public string MusicTitle;

    void Start()
    {
        // ✅ Only change the music if it's not already playing
        if (AudioManager.Instance != null)
        {
            if (AudioManager.Instance.currentMusicTitle != MusicTitle)
            {
                AudioManager.Instance.PlayMusic(MusicTitle);
            }
        }
    }
}
