using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Battery : MonoBehaviour
{
    [System.NonSerialized]
    public float charge = 384f;
    
    [System.NonSerialized]
    public float dischargeFloat = 0.0005f + 0.5f; // Base + Fan usage

    [Header("External scripts")]
    public GameTime timeScript;
    public LightButton lightScript;
    public Tablet tabletScript;
    public ResLightManager camLightManager;

    public Sprite[] batteryLevels;
    
    private int initialTime = 0;
    private float initialCharge;
    private TMP_Text batteryPercentage;

    void Start() {
        initialCharge = charge;
        batteryPercentage = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (initialTime != timeScript.time) {
            if (lightScript.isShiningLeft || lightScript.isShiningRight || camLightManager.hasToggledLights) {
                dischargeFloat = 1f;
            }

            charge = charge - dischargeFloat;

            if (charge < 191f) {
                if (charge < 127f) {
                    if (charge < 63f) {
                        if (charge == 0f) {
                            GetComponent<Image>().sprite = batteryLevels[4]; // Is the battery empty?
                        } else {
                            GetComponent<Image>().sprite = batteryLevels[3]; // Is it less than 63f?
                        }
                    } else {
                        GetComponent<Image>().sprite = batteryLevels[2]; // Is it less than 127f?
                    }
                } else {
                    GetComponent<Image>().sprite = batteryLevels[1]; // Is it less than 191f?
                }
            }

            batteryPercentage.text = string.Format("{0}%", Mathf.FloorToInt((charge / initialCharge) * 100));

            initialTime = timeScript.time;
        }
    }
}
