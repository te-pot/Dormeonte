using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource sfxSource2;

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
        if (Instance == null)
        {
            Instance = this;
            // Optional: keep across scenes
            // DontDestroyOnLoad(gameObject);

            if (musicSource != null && track1 != null)
            {
                musicSource.loop = true;
                musicSource.clip = track1;
                musicSource.Play();
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

    public IEnumerator FadeOutMusic(float duration = 1f)
    {
        if (musicSource == null || !musicSource.isPlaying) yield break;

        float startVolume = musicSource.volume;
        float time = 0;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // reset for future use
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
        sfxSource.Stop();
        sfxSource.clip = clip;
        sfxSource.pitch = 1f;
        sfxSource.Play();
    }

    // Convenience wrappers
    public void PlayTrack1() => PlayMusic(track1);

    public void PlaySFX1() => PlaySFX(sfx1);
    public void PlaySFX2() => PlaySFX(sfx2);
    public void PlaySFX3() => PlaySFX(sfx3);
    public void PlaySFX4() => PlaySFX(sfx4);
    public void PlaySFX5() => PlaySFX(sfx5);

    public void PlaySFX2a() => PlaySFX(sfx2a, true);
}
