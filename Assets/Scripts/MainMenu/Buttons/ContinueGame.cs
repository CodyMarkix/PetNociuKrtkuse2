using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour {
    public void OnButtonPress() {
        if (PlayerPrefs.GetInt("night") <= 5) {
            SceneManager.LoadScene(PlayerPrefs.GetInt("night") + 1);
            Debug.Log(PlayerPrefs.GetInt("night"));
        } else {
            SceneManager.LoadScene(6); // Night 5 scene
        }
    }
}
