using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsBackButton : MonoBehaviour {
    public GameObject mainMenu;
    public GameObject creditsMenu;

    public void OnButtonPress() {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
