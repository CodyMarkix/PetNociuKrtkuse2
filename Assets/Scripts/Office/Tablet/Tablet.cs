using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tablet : MonoBehaviour
{
    public GameObject tabletScreen;
    public RenderTexture[] cameraMaterials;
    public AudioSource tabletOpenSFX;
    
    [System.NonSerialized]
    public bool isLooking = false;
    public string currentCam = "1B";

    public InputActionMap camKeys;

    void OnEnable() {
        camKeys.Enable();
        tabletScreen.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void onDisable() {
        camKeys.Disable();
    }

    void Start() {
        // camKeys.FindAction("Switch Cam 1B").performed += Cam1B;
        // camKeys.FindAction("Switch Cam 2B").performed += Cam2B;
    }

    public int PlayTabletToggle() {
        try {
            tabletOpenSFX.Play();
            return 0;

        } catch (System.Exception) {
            return 1;
        }
    }

    public void Cam1A() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[0];
    }

    public void Cam1B() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[1];
    }

    public void Cam2A() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[2];
    }

    public void Cam2B() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[3];
    }

    public void Cam3A() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[4];
    }

    public void Cam4A() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[5];
    }

    public void Cam5A() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[6];
    }

    public void Cam5B() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[7];
    }

    public void Cam6A() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[8];
    }

    public void Cam6B() {
        tabletScreen.GetComponentInChildren<RawImage>().texture = cameraMaterials[9];
    }

}

