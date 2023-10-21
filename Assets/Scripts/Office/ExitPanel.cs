using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ExitPanel : MonoBehaviour {
    public InputActionMap inputMap;
    public bool isExiting = false;

    void Awake() {
        transform.localScale = new Vector3(0f, 0f, 0f);
        inputMap.FindAction("ConfirmYes").performed += ExitGame;
        inputMap.FindAction("ConfirmNo").performed += CancelExit;
    }

    public void OnPromptShow() {
        isExiting = true;
        transform.localScale = new Vector3(1f, 1f, 1f);

        inputMap.Enable();
    }

    void ExitGame(InputAction.CallbackContext context) {
        Application.Quit();
    }

    void CancelExit(InputAction.CallbackContext context) {
        isExiting = false;
        transform.localScale = new Vector3(0f, 0f, 0f);
    }
}