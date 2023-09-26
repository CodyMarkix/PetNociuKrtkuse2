using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{
    public void OnButtonPress() {
        SceneManager.LoadScene(PlayerPrefs.GetInt("night") + 1);
        Debug.Log(PlayerPrefs.GetInt("night"));
    }
}
