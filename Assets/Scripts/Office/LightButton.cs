using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LightButton : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject[] hallwaylights;
    public Camera playerCamera;
    public int[] test = {0, 1, 2, 3};

    [System.NonSerialized]
    public bool isShiningLeft = false;
    public bool isShiningRight = false;

    private float mousex;
    private float mousey;

    void Start() {
        mousex = Mouse.current.position.x.ReadValue();
        mousey = Mouse.current.position.y.ReadValue();
    }

    void OnMouseDown() {
        toggleLightsOn();
    }

    void OnMouseUp() {
        toggleLightsOff();
    }

    void toggleLightsOn() {
        switch(gameObject.tag) {
            case "DoorButtonLeft":
                hallwaylights[0].GetComponent<Light>().enabled = true;
                isShiningLeft = true;
                break;

            case "DoorButtonRight":
                hallwaylights[1].GetComponent<Light>().enabled = true;
                isShiningRight = true;
                break;

            default:
                Debug.Log("How the fuck bro ._.");
                break;
        }

    }

    // Look not everything has to be super dry ok?

    void toggleLightsOff() {
        switch(gameObject.tag) {
            case "DoorButtonLeft":
                hallwaylights[0].GetComponent<Light>().enabled = false;
                isShiningLeft = true;
                break;

            case "DoorButtonRight":
                hallwaylights[1].GetComponent<Light>().enabled = false;
                isShiningRight = true;
                break;

            default:
                Debug.Log("How the fuck bro ._.");
                break;
        }

    }

}
