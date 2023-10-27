using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Battery : MonoBehaviour {
    [System.NonSerialized]
    // public float charge = 384f;
    public float charge = 480000f;

    [System.NonSerialized]
    // public float dischargeFloat = 0.0008f + 0.65f; // Základ + Větrák
    public float dischargeFloat = 50f + 612f;

    [Header("External scripts")]
    public GameTime timeScript;
    public LightButton[] lightScripts;
    public DoorButton[] doorScripts;
    public Fan fanScript;
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
            if (!isDed) {
                charge = charge - dischargeFloat;
            }

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

            int percentage = Mathf.FloorToInt((charge / initialCharge) * 100) <= 100
                ? Mathf.FloorToInt((charge / initialCharge) * 100)
                : 100;
            
            batteryPercentage.text = string.Format("{0}%", percentage);
            Debug.LogAssertion(string.Format("Charge: {0}; Discharge Float: {1}", charge, dischargeFloat));

            // band-aid patch to get the game out as I promised, working on the issue! >~<
            if (dischargeFloat < 0) {
                /*
                    A negative number can result in battery charge going up and over 100%,
                    still not sure where it comes from.
                */
                dischargeFloat = 50f;

                if (fanScript.isOn) {
                    dischargeFloat = dischargeFloat + 612f;
                }

                if (lightScripts[0].isShiningLeft) {
                    dischargeFloat = dischargeFloat + 625f; 
                }

                if (lightScripts[1].isShiningRight) {
                    dischargeFloat = dischargeFloat + 625f;
                }

                if (!doorScripts[0].doorIsOpen) {
                    dischargeFloat = dischargeFloat + 800f;
                }

                if (!doorScripts[1].doorIsOpen) {
                    dischargeFloat = dischargeFloat + 800f;
                }

                if (camLightManager.hasToggledLights) {
                    dischargeFloat = dischargeFloat + 750f;
                }

            }

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
