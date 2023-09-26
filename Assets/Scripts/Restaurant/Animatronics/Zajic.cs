using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Zajic : MonoBehaviour {
    // A MASSIVE dictionary of all of the animatronic's position, could theoretically put it in a json file
    // and make the program read it, but this the one thing I ABSOLUTELY don't want the user to tamper with without recompiling
    Dictionary<string, Vector3> restaurantPositions = new Dictionary<string, Vector3>() {
        { "podium", new Vector3(2.87f, 1.346f, 17.79f) },
        { "mainArea", new Vector3(5.816f, 0.953f, 10.589f) },
        { "restroom", new Vector3(13.718f, 0.953f, 13.676f) },
        { "kitchen", new Vector3(19.29f, 10.6f, -19.29f) },
        { "hallwayTop", new Vector3(-5.622f, 0.953f, 1.71f) },
        { "hallwayCorner", new Vector3(-6.276f, 0.953f, -10.681f) },
        { "door", new Vector3(-5.78f, 0.953f, -7.36f) }
    };

    Dictionary<string, Vector3> restaurantRotations = new Dictionary<string, Vector3>() {
        { "podium", new Vector3(90f, 180f, 0f) },
        { "mainArea", new Vector3(90f, 180f, 180f) },
        { "restroom", new Vector3(90f, 180f, 299.386f) },
        { "kitchen", new Vector3(90f, 180f, 0f) },
        { "hallwayTop", new Vector3(90f, 180f, 0f) },
        { "hallwayCorner", new Vector3(90f, 180f, 133.593f) },
        { "door", new Vector3(90f, 270f, 57.466f) }
    };

    private List<string> positionIndex = new List<string> { 
        "podium",
        "mainArea",
        "restroom",
        "kitchen",
        "hallwayTop",
        "hallwayCorner",
        "door"
    };

    [Range(0, 20)]
    public int AILevel;
    public bool opportunityRandomness;
    public Fan fanScript;
    public CameraLook camLookScript;
    public Tablet tabletScript;
    public DoorButton doorScript;
    public GameTime gameTimeScript;
    public GameObject realtimeLight;

    private bool movementOpportunity = false;
    private bool canHaveOpportunity = true;
    private int currentPosIndex = 0;
    private int lockAfterDoorAttempt = 0;
    private int opportunitiesInKitchen;
    private Vector3 initialPos;
    System.Random rng = new System.Random();
    
    void Start() {
        StartCoroutine(giveOpportunity());
        initialPos = restaurantPositions["podium"];
        opportunitiesInKitchen = 2 * gameTimeScript.currentNight;
    }

    int getMovementPositon() {
        int randNum = rng.Next(0, 1);

        if (currentPosIndex == 0) {
            return 1;
        } else {
            if (randNum == 0) {
                return -1;
            } else {
                return 1;
            }
        }
    }

    void FixedUpdate() {
        switch (gameTimeScript.currentNight) {
            case 1:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 1;
                        break;

                    case 86 * 3:
                        AILevel = 2;
                        break;

                    case 86 * 4:
                        AILevel = 3;
                        break;
                }
                break;

            case 2:
                switch (gameTimeScript.time) {
                    case 86 * 3:
                        AILevel = 4;
                        break;

                    case 86 * 4:
                        AILevel = 5;
                        break;
                }
                break;

            case 3:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 1;
                        break;
                    
                    case 86 * 3:
                        AILevel = 2;
                        break;
                    
                    case 86 * 4:
                        AILevel = 3;
                        break;
                }
                break;

            case 4:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 3;
                        break;

                    case 86 * 3:
                        AILevel = 4;
                        break;

                    case 86 * 4:
                        AILevel = 5;
                        break;
                }
                break;
            
            case 5:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 6;
                        break;

                    case 86 * 3:
                        AILevel = 7;
                        break;

                    case 86 * 4:
                        AILevel = 8;
                        break;
                }
                break;

            case 6:
                switch (gameTimeScript.time) {
                    case 86 * 2:
                        AILevel = 11;
                        break;

                    case 86 * 3:
                        AILevel = 12;
                        break;

                    case 86 * 4:
                        AILevel = 13;
                        break;
                }
                break;
        }
    }

    IEnumerator giveOpportunity() {
        while (true) {
            yield return new WaitForSeconds(4);

            if (fanScript.getTemperature() > 28) {
                if (lockAfterDoorAttempt > 0) {
                    if (canHaveOpportunity ) {
                        canHaveOpportunity = false;
                        movementOpportunity = true;

                        Debug.Log(string.Format("{0} with AI Level {1} can have an opportunity.", transform.gameObject.name, AILevel));
                        MoveAnimatronic();
                    }
                } else {
                    lockAfterDoorAttempt--;
                }
                canHaveOpportunity = true;
                movementOpportunity = false;
            } else {
                continue;
            }
        }
    }

    void MoveAnimatronic() {
        int randomValue = rng.Next(1, 20);
        if (AILevel >= randomValue) {
            if (currentPosIndex != positionIndex.Count - 1) { // Is the animatronic NOT in a door?
                if (positionIndex[currentPosIndex] == "kitchen") {
                    if (opportunitiesInKitchen > 0) {
                        opportunitiesInKitchen--;
                    }
                } else {
                    Debug.Log(string.Format("{0} with AI Level {1} has moved.", transform.gameObject.name, AILevel));
                    int newPos = currentPosIndex + rng.Next(1, 2);
                    transform.position = restaurantPositions[positionIndex[newPos]];
                    transform.eulerAngles = restaurantRotations[positionIndex[newPos]];
                    currentPosIndex = newPos;
                }
            }  else {
                if (doorScript.doorIsOpen) {
                    if (tabletScript.isLooking) {
                        StartCoroutine(Jumpscare());
                    }
                } else {
                    Debug.Log(string.Format("{0} with AI Level {1} has moved.", transform.gameObject.name, AILevel));
                    int newPos = currentPosIndex - rng.Next(1, 2);
                    transform.position = restaurantPositions[positionIndex[newPos]];
                    transform.eulerAngles = restaurantRotations[positionIndex[newPos]];
                    currentPosIndex = newPos;
                    lockAfterDoorAttempt = 3;
                }
            }
        }
    }

    IEnumerator Jumpscare() {
        PlayerPrefs.SetInt("night", SceneManager.GetActiveScene().buildIndex - 1);
        camLookScript.SwitchTablet();
        GetComponent<Animator>().enabled = true;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.8f);
        SceneManager.LoadScene(7);
    }
}
