using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ambience : MonoBehaviour {
    public AudioSource[] ambiences;
    public bool enableRandomness;
    private int initialTime = 0;
    private int soundToPlay = 0;
    private System.Random rng;

    void Start() {
        rng = new System.Random();
        StartCoroutine(RandomAmbience());
    }

    IEnumerator RandomAmbience() {
        while (true) {
            yield return new WaitForSeconds(60f);
            if (enableRandomness) {
                if (rng.Next(1, 6) > 3) {
                    ambiences[rng.Next(0, ambiences.Length)].Play();
                }
            } else {
                if (soundToPlay == 3) {
                    soundToPlay = 0;
                }
                ambiences[soundToPlay].Play();
                soundToPlay++;
            }
        }
    }
}
