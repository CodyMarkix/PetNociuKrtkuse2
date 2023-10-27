using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Myskus : MonoBehaviour {
        // A MASSIVE dictionary of all of the animatronic's position, could theoretically put it in a json file
    // and make the program read it, but this the one thing I ABSOLUTELY don't want the user to tamper with without recompiling
    Dictionary<string, Vector3> restaurantPositions = new Dictionary<string, Vector3>() {
        { "stage", new Vector3(-4.946f, 1.535f, 12.217f) },
        { "playArea1", new Vector3(-4.17f, 1.057f, 10.03f) },
        { "playArea2" , new Vector3(-3.888f, 1.057f, 8.01f) }
    };

    Dictionary<string, Vector3> restaurantRotations = new Dictionary<string, Vector3>() {
        { "stage", new Vector3(90f, -180f, 0f) },
        { "playArea1", new Vector3(90f, -180f, 23.3f) },
        { "playArea2", new Vector3(90f, -180f, 38.31f) },
    };

    private List<string> positionIndex = new List<string> { 
        "stage",
        "playArea1",
        "playArea2"
    };

    [Range(0, 20)]
    public int AILevel;
    public bool opportunityRandomness;

    [Header("Other scripts")]
    public Fan fanScript;
    public CameraLook camLookScript;
    public Tablet tabletScript;
    public DoorButton doorScript;
    public GameTime gameTimeScript;
    public CameraButtons camButtonsScript;

    [Header("Sounds")]
    public AudioSource jumpscareSFX;
    public AudioSource runSFX;

    [Header("Light")]
    public GameObject realtimeLight;

    private bool movementOpportunity = false;
    private bool canHaveOpportunity = true;
    private int currentPosIndex = 0;
    private Vector3 initialPos;
    private Animator animator;
    System.Random rng = new System.Random();
    
    void Start() {
        StartCoroutine(giveOpportunity());
        animator = transform.gameObject.GetComponent<Animator>();
        initialPos = restaurantPositions["stage"];
    }

    void FixedUpdate() {
        switch (gameTimeScript.currentNight) {
            case 1:
                switch (gameTimeScript.time) {
                    case 86 * 3:
                        AILevel = 1;
                        break;

                    case 86 * 4:
                        AILevel = 2;
                        break;
                }
                break;

            case 2:
                switch (gameTimeScript.time) {
                    case 86 * 3:
                        AILevel = 2;
                        break;

                    case 86 * 4:
                        AILevel = 3;
                        break;
                }
                break;

            case 3:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 2;
                        break;
                    
                    case 86 * 3:
                        AILevel = 3;
                        break;
                    
                    case 86 * 4:
                        AILevel = 4;
                        break;
                }
                break;

            case 4:
                switch (gameTimeScript.time) {
                    case 86 * 3:
                        AILevel = 7;
                        break;

                    case 86 * 4:
                        AILevel = 8;
                        break;
                }
                break;
            
            case 5:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 5;
                        break;

                    case 86 * 3:
                        AILevel = 6;
                        break;

                    case 86 * 4:
                        AILevel = 7;
                        break;
                }
                break;

            case 6:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 16;
                        break;

                    case 86 * 3:
                        AILevel = 17;
                        break;

                    case 86 * 4:
                        AILevel = 18;
                        break;
                }
                break;

            case 7:
                AILevel = PlayerPrefs.GetInt("MyskusAI");
                break;
        }
    }

    IEnumerator giveOpportunity() {
        while (true) {
            yield return new WaitForSeconds(4);
            // Debug.Log(string.Format("AI Level: {0}", AILevel));

                if (canHaveOpportunity) {
                    if (!tabletScript.isLooking) {
                        canHaveOpportunity = false;
                        movementOpportunity = true;
                        
                        // Debug.Log(string.Format("{0} with AI Level {1} can have an opportunity.", transform.gameObject.name, AILevel));
                        MoveAnimatronic();

                        canHaveOpportunity = true;
                        movementOpportunity = false;
                    }
                } else {
                    continue;
                }
        }
    }

    void MoveAnimatronic() {
        int randomValue = rng.Next(1, 20);
        if (AILevel >= randomValue) {
            if (currentPosIndex != positionIndex.Count - 1) { // Is the animatronic ready to jumpscar?
                // Debug.Log(string.Format("{0} with AI Level {1} has moved.", transform.gameObject.name, AILevel));
                int newPos = currentPosIndex + 1;
                transform.position = restaurantPositions[positionIndex[newPos]];
                transform.eulerAngles = restaurantRotations[positionIndex[newPos]];
                currentPosIndex = newPos;
            }  else {
                StartCoroutine(InitiateJumpscare());
            }

        }
    }

    IEnumerator InitiateJumpscare() {
        PlayerPrefs.SetInt("night", SceneManager.GetActiveScene().buildIndex - 2);
        yield return new WaitForSeconds(6f);
        StartCoroutine(RunningAnimation());
    }

    IEnumerator RunningAnimation() {
        if (tabletScript.isLooking) {
            if (tabletScript.currentCam != "Cam1A") {
                // I could just directly call it with something like CameraButtons.ToggleCamButtons() but I'm stupid
                camButtonsScript.ToggleCamButtons("Cam1A");
                tabletScript.currentCam = "Cam1A";
                camButtonsScript.camToggleSFX.Play();
                tabletScript.Cam1A();
            }
        }

        runSFX.Play();
        animator.enabled = true;
        Debug.LogAssertion(string.Format("JUMPSCARE MYSKUS APPROACHING; {0}", doorScript.doorIsOpen));
        yield return new WaitForSeconds(1.8f);
        if (doorScript.doorIsOpen) {
            animator.SetBool("doorIsOpen", true);
            StartCoroutine(Jumpscare());
        } else {
            animator.SetBool("doorIsOpen", false); // For redundancy
            animator.enabled = false;
            runSFX.Stop();

            int newPos = 0;
            transform.position = restaurantPositions[positionIndex[newPos]];
            transform.eulerAngles = restaurantRotations[positionIndex[newPos]];
            currentPosIndex = newPos;
        }
    }

    IEnumerator Jumpscare() {
        Debug.LogAssertion("JUMPSCARE MYSKUS");
        realtimeLight.SetActive(true);
        if (tabletScript.isLooking) {
            camLookScript.SwitchTablet();
        }
        camLookScript.DisableInput(0);
        jumpscareSFX.Play();
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(9);
    }
}
