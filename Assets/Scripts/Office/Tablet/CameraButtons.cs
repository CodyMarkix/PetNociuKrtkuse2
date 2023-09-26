using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraButtons : MonoBehaviour
{
    public GameObject[] camButtons;
    public Sprite[] buttonSprites;
    public Sprite[] buttonSpritesOff;
    
    public GameObject screenPanel;
    public Tablet tabletScript;

    [System.NonSerialized]
    public AudioSource camToggleSFX;

    void Start() {
        camToggleSFX = screenPanel.GetComponent<AudioSource>();
    }

    public void SwitchCam() {
        switch(transform.gameObject.name) {
            case "Cam1A":
                ToggleCamButtons("Cam1A");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[0];
                tabletScript.currentCam = "Cam1A";
                camToggleSFX.Play();
                tabletScript.Cam1A();
                break;

            case "Cam1B":
                ToggleCamButtons("Cam1B");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[1];
                tabletScript.currentCam = "Cam1B";
                camToggleSFX.Play();
                tabletScript.Cam1B();
                break;

            case "Cam2A":
                ToggleCamButtons("Cam2A");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[2];
                tabletScript.currentCam = "Cam2A";
                camToggleSFX.Play();
                tabletScript.Cam2A();
                break;

            case "Cam2B":
                ToggleCamButtons("Cam2B");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[3];
                tabletScript.currentCam = "Cam2B";
                camToggleSFX.Play();
                tabletScript.Cam2B();
                break;

            case "Cam3A":
                ToggleCamButtons("Cam3A");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[4];
                tabletScript.currentCam = "Cam3A";
                camToggleSFX.Play();
                tabletScript.Cam3A();
                break;

            case "Cam4A":
                ToggleCamButtons("Cam4A");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[5];
                tabletScript.currentCam = "Cam4A";
                camToggleSFX.Play();
                tabletScript.Cam4A();
                break;

            case "Cam5A":
                ToggleCamButtons("Cam5A");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[6];
                tabletScript.currentCam = "Cam5A";
                camToggleSFX.Play();
                tabletScript.Cam5A();
                break;

            case "Cam5B":
                ToggleCamButtons("Cam5B");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[7];
                tabletScript.currentCam = "Cam5B";
                camToggleSFX.Play();
                tabletScript.Cam5B();
                break;

            case "Cam6A":
                ToggleCamButtons("Cam6A");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[8];
                tabletScript.currentCam = "Cam6A";
                camToggleSFX.Play();
                tabletScript.Cam6A();
                break;

            case "Cam6B":
                ToggleCamButtons("Cam6B");
                transform.gameObject.GetComponent<Image>().sprite = buttonSprites[9];
                camToggleSFX.Play();
                tabletScript.Cam6B();
                break;

            default:
                Debug.Log("Yeeter má malý pp");
                break;
        }
    }
    
    public void ToggleCamButtons(string camID) {
        for (int i = 0; i < camButtons.Length; i++) {
            if (camButtons[i].name != camID) {
                camButtons[i].GetComponent<Image>().sprite = buttonSpritesOff[i];
            }
        }
    }
}
