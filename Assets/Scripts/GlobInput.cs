using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GlobInput : MonoBehaviour {
    public InputActionAsset globalInput;

    void Awake() {
        globalInput.Enable();
    }

    void Start() {
        globalInput.FindActionMap("Global Input").FindAction("Quit Game").performed += QuitGame;
    }

    void QuitGame(InputAction.CallbackContext context) {
        Application.Quit();
    }
}
