using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

using ETouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class CameraLook : MonoBehaviour
{
    public InputActionAsset inputActions;
    public Animator tabletAnimator;
    public Tablet tabletScript;
    public GameObject tabletScreen;

    private InputActionMap officeKeybinds;
    private InputActionMap officeMouseBinds;

    private Animator anim;
    private bool isLookingAtDoor = false;
    
    private float mousex;
    private float mousey;
    private bool canTransitionMouse = true;
    private bool canToggleTablet = true;
    private bool canDoInput = true;

    void Awake() {
        EnhancedTouchSupport.Enable();
        officeKeybinds = inputActions.FindActionMap("Office input");
        officeMouseBinds = inputActions.FindActionMap("Office input Mouse");
        officeKeybinds.Enable();
        officeMouseBinds.Enable();
    }

    void Start() {
        anim = GetComponent<Animator>();
        officeKeybinds.FindAction("Look left").performed += CamLookLeftKey;
        officeKeybinds.FindAction("Look right").performed += CamLookRightKey;
        officeKeybinds.FindAction("Toggle tablet").performed += ToggleTabletKey;
    }

    void Update() {
        switch (Application.platform) {
            case RuntimePlatform.WindowsEditor or RuntimePlatform.WindowsPlayer:
                mousex = Mouse.current.position.x.ReadValue();
                mousey = Mouse.current.position.y.ReadValue();
                break;

            case RuntimePlatform.Android or RuntimePlatform.IPhonePlayer:
                mousex = ETouch.activeTouches[0].screenPosition.x;
                mousey = ETouch.activeTouches[0].screenPosition.y;
                break;
        }

        if (mousex <= 75f) {
            StartCoroutine(CamLookLeftMouse());
        }

        if (mousex <= Screen.width && mousex >= Screen.width - 75f) {
            StartCoroutine(CamLookRightMouse());
        }

        if (mousex >= 559.5f && mousex <= Screen.width - 561 && mousey <= 60f) {
            StartCoroutine(ToggleTabletMouse());
        }
    }

    void CamLookLeftKey(InputAction.CallbackContext context) {
        SwitchView("left");
    }

    void CamLookRightKey(InputAction.CallbackContext context) {
        SwitchView("right");
    }


    void ToggleTabletKey(InputAction.CallbackContext context) {
        SwitchTablet();
    }

    void SwitchView(string whereToLook) {
        if (canDoInput) {
            switch (whereToLook) {
                case "left":
                    if (!anim.IsInTransition(0)) {
                        switch (anim.GetInteger("Looking")) {
                            case -1:
                                break;
                            
                            case 0:
                                isLookingAtDoor = true;
                                anim.SetInteger("Looking", -1);
                                break;
                            
                            case 1:
                                isLookingAtDoor = false;
                                anim.SetInteger("Looking", 0);
                                break;
                        }
                    }
                    break;
            
                case "right":
                    if (!anim.IsInTransition(0)) {
                            switch (anim.GetInteger("Looking")) {
                                case -1:
                                    isLookingAtDoor = false;
                                    anim.SetInteger("Looking", 0);
                                    break;
                                
                                case 0:
                                    isLookingAtDoor = true;
                                    anim.SetInteger("Looking", 1);
                                    break;
                                
                                case 1:
                                    break;
                            }
                    }
                    break;
            }
        }
    }
    
    public void SwitchTablet() {
        if (canDoInput) {
            if (!isLookingAtDoor) {
                if (!tabletAnimator.IsInTransition(0)) {
                    if (tabletAnimator.GetBool("lookingAtTablet")) {
                        tabletScreen.transform.localScale = new Vector3(0f, 0f, 0f);
                        tabletAnimator.SetBool("lookingAtTablet", false);
                        StartCoroutine(playSFX());
                        tabletScript.isLooking = false;
                    } else {
                        tabletAnimator.SetBool("lookingAtTablet", true);
                        StartCoroutine(playSFX());
                        tabletScript.isLooking = true;
                        StartCoroutine(showScreen()); // Why the fuck can't just Thread.Sleep() work
                    }
                }
            }
        }
    }

    IEnumerator playSFX() {
        yield return new WaitForSeconds(0.3f);
        tabletScript.PlayTabletToggle();
    }

    IEnumerator showScreen() {
        yield return new WaitForSeconds(0.6f);
        tabletScreen.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    IEnumerator CamLookLeftMouse() {
        if (canTransitionMouse && !tabletScript.isLooking) {
            SwitchView("left");
            canTransitionMouse = false;
            yield return new WaitForSeconds(0.45f);
            canTransitionMouse = true;
        } else {
            yield return null;
        }
    }

    IEnumerator CamLookRightMouse() {
        if (canTransitionMouse && !tabletScript.isLooking) {
            SwitchView("right");
            canTransitionMouse = false;
            yield return new WaitForSeconds(0.45f);
            canTransitionMouse = true;
        } else {
            yield return null;
        }
    }

    IEnumerator ToggleTabletMouse() {
        if (canToggleTablet) {
            SwitchTablet();
            canToggleTablet = false;
            yield return new WaitForSeconds(1f);
            canToggleTablet = true;
        } else {
            yield return null;
        }
    }

    // For the spoops (jumpscares)
    public void DisableInput() {
        anim.SetInteger("Looking", 0);
        canDoInput = false;
    }

}
