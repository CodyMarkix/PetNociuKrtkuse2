using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuitButton : MonoBehaviour
{
    new AudioSource audio;

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    public void Quit() {
        Application.Quit();
    }
}
