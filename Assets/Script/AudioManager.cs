using System;
using Unity.VisualScripting;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds, meowSounds;
    public AudioSource musicSource, sfxSource, meowSource;

    [HideInInspector] public string currentMusicTitle = "";


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {

    }

    public void PlayMusic(string name)
    {
        // ✅ If the same music is already playing, do nothing
        if (currentMusicTitle == name && musicSource.isPlaying)
            return;

        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.LogWarning("Music: " + name + " not found!");
            return;
        }

        // ✅ Play only if different music or not playing
        musicSource.clip = s.clip;
        musicSource.loop = true;
        musicSource.Play();

        currentMusicTitle = name; // remember the track
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void PlayMeow(string name)
    {
        Sound s = Array.Find(meowSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        // Stop any current playback
        meowSource.Stop();

        // Assign and play the clip directly
        meowSource.clip = s.clip;
        meowSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
        musicSource.clip = null;
    }




}
