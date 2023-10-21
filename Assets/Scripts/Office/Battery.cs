using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Battery : MonoBehaviour {
    [System.NonSerialized]
    public float charge = 384f;
    
    [System.NonSerialized]
    public float dischargeFloat = 0.0005f + 0.5f; // Základ + Větrák

    [Header("External scripts")]
    public GameTime timeScript;
    public LightButton lightScript;
    public Tablet tabletScript;
    public ResLightManager camLightManager;
    public CameraLook camLookScript;
    public PowerOutage powerOutageScript;
    public LightMapMgr lightMapMgrScript;

    public Sprite[] batteryLevels;

    public InputActionMap map;
    
    private int initialTime = 0;
    private float initialCharge;
    private bool isDed = false; // Why is the heavy dead?!
    private TMP_Text batteryPercentage;

    void Start() {
        initialCharge = charge;
        batteryPercentage = GetComponentInChildren<TMP_Text>();
        map.Enable();
        map.FindAction("losePower").performed += OnBatteryZeroBind;
    }

    // Update is called once per frame
    void Update() {
        if (initialTime != timeScript.time) {
            charge = charge - dischargeFloat;

            if (charge < 288f) {
                if (charge < 192f) {
                    if (charge < 96f) {
                        if (charge <= 0f) {
                            if (!isDed) {
                                GetComponent<Image>().sprite = batteryLevels[4]; // Baterie je prázdná
                                OnBatteryZero();
                                isDed = true;
                            }
                        } else {
                            GetComponent<Image>().sprite = batteryLevels[3]; // Zbývá čtvrt baterie
                        }
                    } else {
                        GetComponent<Image>().sprite = batteryLevels[2]; // Zbývá polovina baterie
                    }
                } else {
                    GetComponent<Image>().sprite = batteryLevels[1]; // Baterie je plná
                }
            }

            batteryPercentage.text = string.Format("{0}%", Mathf.FloorToInt((charge / initialCharge) * 100));
            Debug.Log(string.Format("Charge: {0}; Discharge Float: {1}", charge, dischargeFloat));

            initialTime = timeScript.time;
        }
    }

    void OnBatteryZero() {
        camLookScript.DisableInput(1);
        lightMapMgrScript.SwitchToDark();
        powerOutageScript.PowerOutageAnimation();
    }

    void OnBatteryZeroBind(InputAction.CallbackContext context) {
        if (Debug.isDebugBuild) {
            OnBatteryZero();
        }
    }
}
