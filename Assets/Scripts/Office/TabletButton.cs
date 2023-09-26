using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TabletButton : MonoBehaviour
{
    public GameObject playercamera;
    private Animator camanim;

    void Start() {
        camanim = playercamera.GetComponent<Animator>();
    }

    public void toggleCamera() {
        if (camanim.GetBool("OpenCamera")) {
            camanim.SetBool("OpenCamera", false);
        } else {
            camanim.SetBool("OpenCamera", true);
        }
    }
}
