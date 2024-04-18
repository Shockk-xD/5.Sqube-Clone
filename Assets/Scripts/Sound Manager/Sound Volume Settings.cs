using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    private void Start()
    {
        
        if (PlayerPrefs.HasKey("Music Volume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }

    public void SetMusicVolume()
    {
        float volume = _musicSlider.value;
        _audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Music Volume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = _sfxSlider.value;
        _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 60 + 20);
        PlayerPrefs.SetFloat("SFX Volume", volume);
    }

    private void LoadVolume()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("Music Volume");
        _sfxSlider.value = PlayerPrefs.GetFloat("SFX Volume");
        
        SetMusicVolume();
        SetSFXVolume();
    }
}
