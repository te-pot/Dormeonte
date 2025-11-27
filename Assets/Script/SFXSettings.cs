using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFXSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider SFXSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetSFXVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        audioMixer.SetFloat("ExposedSFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void LoadVolume()
    {
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        SetSFXVolume();
    }
}
