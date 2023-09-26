using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroWarning : MonoBehaviour {
    public Animator[] animators;

    // Start is called before the first frame update
    void Start() {
        foreach (Animator x in animators) {
            x.SetBool("SeenWarning", false);
        }
        StartCoroutine(FadeKrtkus());
    }

    IEnumerator FadeKrtkus() {
        yield return new WaitForSeconds(2.5f);
        foreach (Animator x in animators) {
            x.SetBool("SeenWarning", true);
        }
        StartCoroutine(SetScene());
    }

    IEnumerator SetScene() {
        yield return new WaitForSeconds(1);
        if (PlayerPrefs.GetInt("started") == 1) {
            SceneManager.LoadScene(1);
        } else {
            SceneManager.LoadScene(10);
        }
    }
}
