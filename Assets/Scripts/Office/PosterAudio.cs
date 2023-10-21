using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterAudio : MonoBehaviour {
    [Header("Audio")]
    public AudioSource flakanec;
    public AudioSource ozralej;

    [Header("External Scripts")]
    public Tablet tabletScript;

    private System.Random rng;

    void Awake() {
        rng = new System.Random();
    }

    void OnMouseDown() {
        if (!tabletScript.isLooking) {
            int randNum = rng.Next(0, 10);
            Debug.Log(randNum);

            if (randNum >= 5) {
                flakanec.Play();
            } else {
                ozralej.Play();
            }
        }
    }
}
