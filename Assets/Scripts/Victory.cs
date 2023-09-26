using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour {
    void Start() {
        StartCoroutine(GoBackToMenu());
    }

    IEnumerator GoBackToMenu() {
        yield return new WaitForSeconds(13.470f);
        SceneManager.LoadScene(1);
    }
}
