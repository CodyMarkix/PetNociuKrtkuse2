using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterAudio : MonoBehaviour {
    [Header("Audio")]
    public AudioSource flakanec;
    public AudioSource ozralej;
    public AudioSource plysovyMimon;

    [Header("External Scripts")]
    public Tablet tabletScript;

    private System.Random rng;

    void Awake() {
        rng = new System.Random();
    }

    void OnMouseDown() {
        if (!tabletScript.isLooking) {
            int randNum = rng.Next(0, 18);

            if (randNum >= 0 && randNum <= 6) {
                flakanec.Play();
            } else if (randNum >= 7 && randNum <= 12) {
                ozralej.Play();
            } else if (randNum >= 13 && randNum <= 18) {
                plysovyMimon.Play();
            }
        }
    }
}
