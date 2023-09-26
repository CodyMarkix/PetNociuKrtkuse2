using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour {
    public GameObject[] doors;
    public Battery batteryScript;

    [System.NonSerialized]
    public bool doorIsOpen = true;

    void OnMouseDown() {
        if (gameObject.tag == "DoorButtonLeft") {
            
            if (doors[0].GetComponent<Animator>().GetBool("buttonPressed")) {
                doors[0].GetComponent<Animator>().SetBool("buttonPressed", false);
                batteryScript.dischargeFloat--;
                doorIsOpen = true;
            } else {
                doors[0].GetComponent<Animator>().SetBool("buttonPressed", true);
                batteryScript.dischargeFloat++;
                doorIsOpen = false;
            }
        } else if (gameObject.tag == "DoorButtonRight") {

            if (doors[1].GetComponent<Animator>().GetBool("buttonPressed")) {
                doors[1].GetComponent<Animator>().SetBool("buttonPressed", false);
                batteryScript.dischargeFloat--;
                doorIsOpen = true;
            } else {
                doors[1].GetComponent<Animator>().SetBool("buttonPressed", true);
                batteryScript.dischargeFloat++;
                doorIsOpen = false;
            }
        }
    }
}
