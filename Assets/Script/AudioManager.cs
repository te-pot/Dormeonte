using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("----------Audio Source-----------")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource sfxSource;

    [Header("----------Audio Clip-----------")]
    public AudioClip background;
    public AudioClip bounce;
    public AudioClip ghostDie;
    public AudioClip track3;

    void Awake()
    {
        // 🧠 Singleton logic — replace the old one if a new scene has its own AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // keep through scene loads
        }
        else if (Instance != this)
        {
            // 🔄 A new AudioManager appeared — destroy the old one and replace it
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    void Start()
    {
        if (background != null)
        {
            PlayMusic(background);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null || musicSource == null) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.pitch = Random.Range(0.95f, 1.05f);
        sfxSource.PlayOneShot(clip);
    }



    public void PlayBounce()
    {
        PlaySFX(bounce);
    }

    public void PlayGhostDie()
    {
        PlaySFX(ghostDie);
    }

    public void PlayTrack3()
    {
        PlaySFX(track3);
    }
}
