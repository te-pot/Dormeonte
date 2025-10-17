using UnityEngine;

public class EndingSceneAudioManager : MonoBehaviour
{
    public static EndingSceneAudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;   // For background music
    public AudioSource sfxSource;     // For one-shot SFX

    [Header("Music Clip")]
    public AudioClip endingMusic;     // Optional ending music

    [Header("SFX Clips")]
    public AudioClip sfx1; // High score
    public AudioClip sfx2; // Medium score
    public AudioClip sfx3; // Low score

    void Awake()
    {
        // Destroy any existing gameplay AudioManager to prevent conflicts
        AudioManager oldAM = FindObjectOfType<AudioManager>();
        if (oldAM != null)
        {
            Destroy(oldAM.gameObject);
        }

        // Singleton setup for ending scene manager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // Play ending music if assigned
        PlayMusic();

        // Play SFX based on score
        if (ScoreManager.Instance == null)
        {
            Debug.LogWarning("ScoreManager instance not found!");
            return;
        }

        int score = ScoreManager.Instance.GetScore();

        if (score >= 120)
            PlaySFX(1);
        else if (score >= 100)
            PlaySFX(2);
        else
            PlaySFX(3);
    }

    /// <summary>
    /// Play background music
    /// </summary>
    public void PlayMusic()
    {
        if (musicSource == null || endingMusic == null) return;

        musicSource.clip = endingMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    /// <summary>
    /// Stops music immediately
    /// </summary>
    public void StopMusic()
    {
        if (musicSource == null) return;

        musicSource.Stop();
        musicSource.clip = null;
    }

    /// <summary>
    /// Plays one of the SFX clips
    /// </summary>
    /// <param name="clipNumber">1 = high, 2 = medium, 3 = low</param>
    public void PlaySFX(int clipNumber)
    {
        if (sfxSource == null) return;

        AudioClip clipToPlay = null;

        switch (clipNumber)
        {
            case 1: clipToPlay = sfx1; break;
            case 2: clipToPlay = sfx2; break;
            case 3: clipToPlay = sfx3; break;
        }

        if (clipToPlay != null)
        {
            sfxSource.pitch = Random.Range(0.95f, 1.05f);
            sfxSource.PlayOneShot(clipToPlay);
        }
    }
}
