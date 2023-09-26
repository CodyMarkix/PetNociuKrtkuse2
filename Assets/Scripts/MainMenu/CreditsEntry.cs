using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsEntry : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject creditsMenu;

    public void OnButtonPress() {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
}
