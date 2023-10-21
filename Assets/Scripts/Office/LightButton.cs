using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightButton : MonoBehaviour {
    [Header("Game Objects")]
    public GameObject[] hallwaylights;
    public Camera playerCamera;
    public PowerOutage powerOutageScript;
    public int[] test = {0, 1, 2, 3};

    [Header("External Scripts")]
    public Battery batteryScript;

    [System.NonSerialized]
    public bool isShiningLeft = false;
    public bool isShiningRight = false;

    private float mousex;
    private float mousey;

    void Start() {
        mousex = Mouse.current.position.x.ReadValue();
        mousey = Mouse.current.position.y.ReadValue();
    }

    void OnMouseDrag() {
        toggleLightsOn();
    }

    void OnMouseUp() {
        toggleLightsOff();
    }

    void toggleLightsOn() {
        if (!powerOutageScript.isPowerOut) {
            switch(gameObject.tag) {
                case "DoorButtonLeft":
                    hallwaylights[0].GetComponent<Light>().enabled = true;
                    batteryScript.dischargeFloat = batteryScript.dischargeFloat + 0.6f;
                    isShiningLeft = true;
                    break;

                case "DoorButtonRight":
                    hallwaylights[1].GetComponent<Light>().enabled = true;
                    batteryScript.dischargeFloat = batteryScript.dischargeFloat + 0.5f;
                    isShiningRight = true;
                    break;

                default:
                    Debug.Log("How the fuck bro ._.");
                    break;
            }
        }

    }

    // Prosimtě, ne každej kód musí bejt extrémně suchej, ok?

    void toggleLightsOff() {
        if (!powerOutageScript.isPowerOut) {
            switch(gameObject.tag) {
                case "DoorButtonLeft":
                    hallwaylights[0].GetComponent<Light>().enabled = false;
                    batteryScript.dischargeFloat = batteryScript.dischargeFloat - 0.5f;
                    isShiningLeft = true;
                    break;

                case "DoorButtonRight":
                    hallwaylights[1].GetComponent<Light>().enabled = false;
                    batteryScript.dischargeFloat = batteryScript.dischargeFloat - 1f;
                    isShiningRight = true;
                    break;

                default:
                    Debug.Log("How the fuck bro ._.");
                    break;
            }
        }

    }

}
