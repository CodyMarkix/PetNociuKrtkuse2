using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGuy : MonoBehaviour {
    public AudioSource[] audioFiles = new AudioSource[3];
    public GameObject hangUpButton;
    public float transcriptLength;

    void Awake() {
        hangUpButton.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    void Start()
    {
        StartCoroutine(CallPart1());
    }

    IEnumerator CallPart1() {
        audioFiles[0].Play();
        yield return new WaitForSeconds(3.8f);
        audioFiles[1].Play();
        StartCoroutine(CallPart2());
    }

    IEnumerator CallPart2() {
        audioFiles[2].Play();
        hangUpButton.transform.localScale = new Vector3(1f, 1f, 1f);
        yield return new WaitForSeconds(transcriptLength);
        audioFiles[3].Play();   
        hangUpButton.SetActive(false);
    }

    public void OnButtonPress() {
        audioFiles[2].Stop();
        audioFiles[3].Play();
        hangUpButton.SetActive(false);
    }

}
