using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GlobInput : MonoBehaviour {
    public InputActionAsset globalInput;
    public ExitPanel exitPanelScript;

    void Awake() {
        globalInput.Enable();
    }

    void Start() {
        globalInput.FindActionMap("Global Input").FindAction("Quit Game").performed += QuitGame;
    }

    void QuitGame(InputAction.CallbackContext context) {
        int[] nightIDs = {
            2, 3, 4, 5, 6, 7, 8
        };
        bool isNight = false;
        int currentNight = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < nightIDs.Length; i++) {
            if (nightIDs[i] == currentNight) {
                isNight = true;
            }
        }

        if (!isNight) {
            Application.Quit();
        } else {
            if (!exitPanelScript.isExiting) {
                exitPanelScript.OnPromptShow();
            }
        }
    }
}
