using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwitchToMenu());
    }

    IEnumerator SwitchToMenu() {
        yield return new WaitForSeconds(58.123f);
        Debug.Log("Audio played");
        
        PlayerPrefs.SetInt("started", 1);
        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }
}
