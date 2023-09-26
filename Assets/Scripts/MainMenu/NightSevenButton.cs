using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class NightSevenButton : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject customnight;
    public Camera ppcamera; // Hehe, pp (Fr, that means "Post-Processing")

    public void OnButtonPress() {
        mainmenu.SetActive(false);
        ppcamera.GetComponent<PostProcessVolume>().enabled = false;
        customnight.SetActive(true);
    }
}
