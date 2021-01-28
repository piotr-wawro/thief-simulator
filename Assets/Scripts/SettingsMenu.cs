using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TextMeshProUGUI textPOV;
    public TextMeshProUGUI textSensitivity;

    public Slider sliderGeneralVol;
    public Slider sliderMusicVol; 
    public Slider sliderEffectsVol;
    public Slider sliderPOV;
    public Slider sliderSensitivity;

    private void Start()
    {
        var generalVol = PlayerPrefs.GetFloat("Master volume",0);
        var musicVol = PlayerPrefs.GetFloat("Music volume",0);
        var effectsVol = PlayerPrefs.GetFloat("Effects volume",0);
        var sensitivity = PlayerPrefs.GetFloat("Mouse sensitivity",0);
        var pov = PlayerPrefs.GetFloat("FOV",0);
        
        if (sensitivity != 0)
        {
            sliderSensitivity.value = sensitivity;
            textSensitivity.text = sensitivity.ToString("N2");
        }
        if (pov != 0)
        {
            sliderPOV.value = pov;
            textPOV.text = pov + "°";
        }
        if (generalVol != 0)
        {
            sliderGeneralVol.value = generalVol;
        }
        if (musicVol != 0)
        {
            sliderMusicVol.value = musicVol;
        }
        if (effectsVol != 0)
        {
            sliderEffectsVol.value = effectsVol;
        }

    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Master volume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("Music volume", Mathf.Log10(volume) * 20);
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("Effects volume", Mathf.Log10(volume) * 20);
    }

    public void SetFOV(float value)
    {
        textPOV.text = value + "°";
        PlayerPrefs.SetFloat("FOV", value);      
    }

    public void SetMouseSensitivity(float mouseSensitivity)
    {
        textSensitivity.text = mouseSensitivity.ToString("N2");
        PlayerPrefs.SetFloat("Mouse sensitivity", mouseSensitivity);
    }
}
