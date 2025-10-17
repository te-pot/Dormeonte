using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;      // Default SFX source
    public AudioSource sfxSource2;     // Optional SFX source for projectiles, etc.

    [Header("Music Tracks")]
    public AudioClip track1;

    [Header("SFX Tracks")]
    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;
    public AudioClip sfx5;
    public AudioClip sfx2a; // projectile

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            // Optional: remove DontDestroyOnLoad if you want scene-specific AudioManagers
            // DontDestroyOnLoad(gameObject);

            // Start music immediately
            if (musicSource != null && track1 != null)
            {
                musicSource.loop = true;
                musicSource.PlayOneShot(track1);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // ---------------- MUSIC ----------------
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (musicSource == null || clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource == null) return;
        musicSource.Stop();
        musicSource.clip = null;
    }

    // ---------------- SFX ----------------
    public void PlaySFX(AudioClip clip, bool useSecondarySource = false)
    {
        AudioSource source = useSecondarySource ? sfxSource2 : sfxSource;
        if (source == null || clip == null) return;

        source.pitch = Random.Range(0.95f, 1.05f);
        source.PlayOneShot(clip);
    }

    public void PlaySFXImmediate(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.Stop();         // Stop any clip currently playing
        sfxSource.clip = clip;    // Assign the new clip
        sfxSource.pitch = 1f;     // or add slight variation if you want
        sfxSource.Play();         // Play from the start
    }


    // Convenience wrappers
    public void PlayTrack1() => PlayMusic(track1);

    public void PlaySFX1() => PlaySFX(sfx1);
    public void PlaySFX2() => PlaySFX(sfx2);
    public void PlaySFX3() => PlaySFX(sfx3);
    public void PlaySFX4() => PlaySFX(sfx4);
    public void PlaySFX5() => PlaySFX(sfx5);

    // Special SFX using secondary source
    public void PlaySFX2a() => PlaySFX(sfx2a, true);
}
