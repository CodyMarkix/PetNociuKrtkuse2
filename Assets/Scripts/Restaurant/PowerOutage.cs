using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOutage : MonoBehaviour {
    [Header("Audio")]
    public AudioSource powerOutSound;
    public AudioSource powerOutMusic;

    [Header("Game Objects")]
    public GameObject krtkusEyes;
    public GameObject MainCamera;

    [Header("UI Elements")]
    public GameObject[] UIElements;

    [Header("External scripts")]
    public Krtkus krtkusScript;
    public Fan fanScript;
    public CameraLook cameraLookScript;

    [System.NonSerialized]
    public bool isPowerOut = false;

    private System.Random rng;

    void Start() {
        rng = new System.Random();
    }

    public void PowerOutageAnimation() {
        foreach (GameObject x in UIElements) {
            x.transform.localScale = new Vector3(0f, 0f, 0f);
        }

        isPowerOut = true;
        StartCoroutine(PlayPowerOutageSound());
    }

    IEnumerator PlayPowerOutageSound() {
        fanScript.GetComponent<AudioSource>().Stop();
        powerOutSound.Play();
        MainCamera.GetComponents<AudioSource>()[1].Play();
        yield return new WaitForSeconds(4.749f);
        StartCoroutine(PlaySusMusic());
    }

    IEnumerator PlaySusMusic() {
        Animator animator = GetComponent<Animator>();

        animator.enabled = true;
        powerOutMusic.Play();
        float waitingTime = (float)(rng.NextDouble() * (7 - 5) + 5);  // 5 = minimum amount of seconds, 7 = maximum amount of seconds
        yield return new WaitForSeconds(waitingTime);

        animator.enabled = false;
        powerOutMusic.Stop();
        krtkusEyes.GetComponent<MeshRenderer>().enabled = false;
        StartCoroutine(Jumpscare());
    }

    IEnumerator Jumpscare() {
        cameraLookScript.anim.SetInteger("Looking", 0);
        yield return new WaitForSeconds((float)(rng.NextDouble() * (7 - 3) + 3));
        StartCoroutine(krtkusScript.Jumpscare());
    }
}
