using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour
{
    [Header("Audio Slider Scripts")]
    public AudioSlider[] sliderScripts;

    public void Save() {
        foreach(AudioSlider slider in sliderScripts) {
            PlayerPrefs.SetFloat(slider.exposedParameter, slider.sliderValue);
        }
    }

    public void LoadSettings() {
        foreach (AudioSlider slider in sliderScripts) {
            float value = PlayerPrefs.GetFloat(slider.exposedParameter);
            slider.setSliderValue(value);
        }
    }
}
