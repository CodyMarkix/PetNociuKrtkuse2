using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fan : MonoBehaviour {
    [Header("Input")]
    public InputActionAsset inputActions;
    
    [Header("Animation (UI)")]
    public GameObject fanIcon;
    public Animator iconAnimator;
    public GameObject tempValueText;
    public GameObject deathPanel;
    public GameObject heatPanel;

    [Header("Animation")]
    public Animator selfAnimator;
    
    [Header("Scripts")]
    public GameTime timeScript;
    public Battery batteryScript;
    
    [System.NonSerialized]
    public bool isOn = true;

    private int initialTime = 0;
    private int _temperature = 20;
    private AudioSource fanAudio;
    private float heatTransparency = 0f;
    private int canIncreaseTemp = 0;
    private int alphaChannelMultiplier = 1;

    public int getTemperature() {
        return _temperature;
    }

    private int setTemperature(int newTemp) {
        _temperature = newTemp;
        return _temperature;
    }

    void Awake() {
        inputActions.FindAction("Toggle Fan").performed += toggleFan;
    }

    void OnEnable() {
        inputActions.FindAction("Toggle Fan").Enable();
        fanAudio = GetComponent<AudioSource>();
    }

    void Update() {
        if (initialTime != timeScript.time) {
            initialTime = timeScript.time;
            if (isOn) {
                if (getTemperature() > 20) {
                    if (canIncreaseTemp == 0) {
                        canIncreaseTemp++;
                    } else {
                        setTemperature(getTemperature() - 1);
                        if (getTemperature() > 35) {
                            SetAlphaChannel(0.025f * alphaChannelMultiplier, heatPanel.GetComponent<Image>());
                            alphaChannelMultiplier--;
                        } else if (getTemperature() == 35) {
                            SetAlphaChannel(0f, heatPanel.GetComponent<Image>());
                        }
                        canIncreaseTemp = 0;
                    }
                }
            } else {
                if (getTemperature() < 48) {
                    if (canIncreaseTemp == 0) {
                        canIncreaseTemp++;
                    } else {
                        setTemperature(getTemperature() + 1);
                        if (getTemperature() > 35) {
                            SetAlphaChannel(0.025f * alphaChannelMultiplier, heatPanel.GetComponent<Image>());
                            alphaChannelMultiplier++;
                        }
                        canIncreaseTemp = 0;
                    }
                } else {
                    StartCoroutine(OnFanGoTooHigh());
                }
            }

            tempValueText.GetComponent<TMPro.TMP_Text>().text = getTemperature().ToString();
        }
    }

    void fadeOutFan() {
        for (float i = 1f; i > 0; i--) {
            fanAudio.volume = i * UnityEngine.Time.deltaTime;
        }
    }

    void fadeInFan() {
        for (float i = 0f; i < 1f; i++) {
            fanAudio.volume = i;
        }
    }

    void toggleFan(InputAction.CallbackContext context) {
        if (isOn) {
            // fadeOutFan();
            fanAudio.Stop();
            iconAnimator.SetBool("disabledFan", true);
            selfAnimator.SetBool("fanIsOn", false);
            // batteryScript.dischargeFloat = batteryScript.dischargeFloat - 0.8f;
            batteryScript.dischargeFloat = batteryScript.dischargeFloat - 612f;
            isOn = !isOn;
        } else {
            // fadeInFan();
            fanAudio.Play();
            iconAnimator.SetBool("disabledFan", false);
            selfAnimator.SetBool("fanIsOn", true);
            // batteryScript.dischargeFloat = batteryScript.dischargeFloat + 0.8f;
            batteryScript.dischargeFloat = batteryScript.dischargeFloat + 612f;
            isOn = !isOn;
        }
    }

    IEnumerator OnFanGoTooHigh() {
        deathPanel.SetActive(true);
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(9);
    }

    void SetAlphaChannel(float alpha, Image img) {
        Color newColor = img.color;
        newColor.a = alpha;
        img.color = newColor;
    }
}
