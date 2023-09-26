using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour {
    public GameObject gameOverObj;
    public GameObject whiteNoiseImg;

    // Start is called before the first frame update
    void Start() {
        whiteNoiseImg.SetActive(false);
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText() {
        yield return new WaitForSeconds(7f);
        gameOverObj.SetActive(true);
        StartCoroutine(ExitToMenu());
    }

    IEnumerator ExitToMenu() {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
