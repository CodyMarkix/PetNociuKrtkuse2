using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResLightManager : MonoBehaviour {
    public GameObject[] lights;

    [Header("External scripts")]
    public Tablet tabletScript;
    public Battery batteryScript;

    [Header("Input")]
    public InputAction toggleLightInput;

    [System.NonSerialized]
    public bool hasToggledLights = false;

    private bool lastRememberedState = false;
    
    void Awake() {
        toggleLightInput.started += EnableLight;
        toggleLightInput.canceled += DisableLight;
    }

    void Update() {
        if (lastRememberedState != tabletScript.isLooking) {
            if (tabletScript.isLooking) {
                toggleLightInput.Enable();
            } else {
                toggleLightInput.Disable();
            }

            lastRememberedState = tabletScript.isLooking;
        }
    }


    void EnableLight(InputAction.CallbackContext context) {
        switch (tabletScript.currentCam) {
            case "Cam1A":
                lights[0].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam1B":
                lights[1].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam2A":
                lights[2].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam2B":
                lights[3].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam3A":
                lights[4].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam4A":
                lights[5].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam5A":
                lights[6].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam5B":
                lights[7].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;

            case "Cam6A":
                lights[8].SetActive(true);
                hasToggledLights = true;
                batteryScript.dischargeFloat = batteryScript.dischargeFloat + 750f; // 0.6f
                break;
        }
    }

    void DisableLight(InputAction.CallbackContext context) {
        batteryScript.dischargeFloat = batteryScript.dischargeFloat - 750f; // 0.6f
        hasToggledLights = false;
        foreach (GameObject x in lights) {
            x.SetActive(false);
        }
    }
}
