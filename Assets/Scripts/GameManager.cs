using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour {
    public GameObject customnightmenu;
    public GameObject nightsixbutton;
    public GameObject nightsevenbutton;

    public InputAction action;

    [System.NonSerialized]
    public int night; // 0 = in main menu; 1 - 5 = night 1 - 5

    // Start is called before the first frame update
    void Start() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;

        if (currentScene == 0) {
            if (!PlayerPrefs.HasKey("night")) {
                SetUpPlayerPrefs();
            }
        } else if (currentScene == 1) {
            if (PlayerPrefs.GetInt("started") != 1) { PlayerPrefs.SetInt("started", 1); }

            action.Enable();
            action.performed += SwitchToTrailer;
            
            night = PlayerPrefs.GetInt("night");

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
                Screen.orientation = ScreenOrientation.LandscapeLeft;
            }
            
            if (PlayerPrefs.GetInt("night") < 5) { nightsixbutton.SetActive(false); }
            if (PlayerPrefs.GetInt("night") < 6) { nightsevenbutton.SetActive(false); }

            customnightmenu.SetActive(false);
        }
    }

    void SwitchToTrailer(InputAction.CallbackContext context) {
        SceneManager.LoadScene(4);
    }

    // Update is called once per frame
    void QuitGame(InputAction.CallbackContext context) {
        PlayerPrefs.Save();
        Application.Quit();
    }

    void SetUpPlayerPrefs() {
        if (!PlayerPrefs.HasKey("night")) { PlayerPrefs.SetInt("night", 1); }
        PlayerPrefs.Save();
    }
}
