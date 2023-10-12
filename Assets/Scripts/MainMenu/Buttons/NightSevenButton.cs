using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NightSevenButton : MonoBehaviour {
    public GameObject mainmenu;
    public GameObject customnight;

    public void OnButtonPress() {
        mainmenu.SetActive(false);
        customnight.SetActive(true);
    }
}
