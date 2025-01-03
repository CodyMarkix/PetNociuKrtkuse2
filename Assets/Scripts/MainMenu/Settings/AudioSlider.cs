using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public AudioMixer mixer;
    public AudioSource blipSFX;
    public AudioSource bopSFX;
    public string exposedParameter;
    [Range(-20, 20)]
    public float offset; // logarithmic
    
    [System.NonSerialized]
    public float sliderValue;

    void Update() {
        sliderValue = gameObject.GetComponent<Slider>().value;
    }

    public void OnSliderChange() {
        Debug.Log(sliderValue);
        mixer.SetFloat(exposedParameter, (Mathf.Log10(sliderValue) * 20) + offset);
    }

    public void setSliderValue(float value) {
        sliderValue = value;
        OnSliderChange();
    }

    public void OnPointerDown(PointerEventData data) {
        blipSFX.Play(0);
    }

    public void OnPointerUp(PointerEventData data) {
        bopSFX.Play(0);
    }
}
