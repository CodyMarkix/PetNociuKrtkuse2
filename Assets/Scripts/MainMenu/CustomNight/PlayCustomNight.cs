using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCustomNight : MonoBehaviour {
    // 0 = Krtkus; 1 = Myskus; 2 = Zajic
    public AILevelCheck[] levelCheckScripts;

    public void SwitchToNight() {
        PlayerPrefs.SetInt("KrtkusAI", levelCheckScripts[0].chosenAILevel);
        PlayerPrefs.SetInt("MyskusAI", levelCheckScripts[1].chosenAILevel);
        PlayerPrefs.SetInt("ZajicAI", levelCheckScripts[2].chosenAILevel);
        PlayerPrefs.Save();

        SceneManager.LoadScene(8);
    }
}
