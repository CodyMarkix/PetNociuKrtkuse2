using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject previousMenu;

    public void OnButtonPress() {
        previousMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
