using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class SixAM : MonoBehaviour {
    public AudioSource endNightSound;
    public GameObject endNightPanel;

    [Header("Animatronics")]
    public Krtkus krtkusak;
    public Myskus myskusak;
    public Zajic zajac;

    public InputActionMap map;
    System.Random rng = new System.Random();

    List<string> alphabet = new List<string>();

    void Awake() {
        map.Enable();
        map.FindAction("SkipNight").performed += EndNightKeybind;
    }

    void Start() {
        for (char c = 'A'; c <= 'Z'; c++) {
            alphabet.Add("" + c);
        }
        for (char i = '0'; i <= '9'; i++) {
            alphabet.Add("" + i);
        }

        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }

    public void EndNightKeybind(InputAction.CallbackContext context) {
        if (Debug.isDebugBuild) {
            EndNight();
        }
    }

    public void EndNight() {
        krtkusak.enabled = false;
        myskusak.enabled = false;
        zajac.enabled = false;

        endNightPanel.SetActive(true);
        endNightSound.Play();
        StartCoroutine(SwitchNextNight());
    }

    IEnumerator SwitchNextNight() {
        int scene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        int currentNight = scene - 1;
        int nextNight = scene;

        Debug.Log(string.Format("Current scene: {0}; Next scene: {1} Current night: {2}; Next night: {3}", scene, nextScene, currentNight, nextNight));

        StartCoroutine(SetRandomNumbers());
        yield return new WaitForSeconds(10f);
        if (scene <= 5 && scene >= 2 ) {
            // Načte další noc
            PlayerPrefs.SetInt("night", nextNight);
            PlayerPrefs.Save();

            SceneManager.LoadScene(nextScene);
        } else if (scene == 6) {
            // Načte victory scénu
            PlayerPrefs.SetInt("night", nextNight);
            PlayerPrefs.Save();

            SceneManager.LoadScene(10);
        } else if (scene == 7 || scene == 8) {
            // Načte main menu
            if (scene == 7) {
                if (PlayerPrefs.GetInt("night") == 6) {
                    PlayerPrefs.SetInt("night", nextNight);
                }
            } else if (scene == 8) {
                if (krtkusak.AILevel == 20 && myskusak.AILevel == 20 && zajac.AILevel == 20) {
                    PlayerPrefs.SetInt("night", scene); // Scene should be equal to 8 at that moment
                }
            }

            PlayerPrefs.Save();
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator SetRandomNumbers() {
        int iterator = 1;
        while (true) {
            yield return new WaitForSeconds(0.25f);
            endNightPanel.GetComponentInChildren<TMP_Text>().text = string.Format("{0} {1}{2}",
                                                                        alphabet[rng.Next(0, alphabet.Count - 1)],
                                                                        alphabet[rng.Next(0, alphabet.Count - 1)],
                                                                        alphabet[rng.Next(0, alphabet.Count - 1)]
                                                                    );
            if (iterator == 17) {
                break;
            } else {
                iterator++;
            }
        }

        endNightPanel.GetComponentInChildren<TMP_Text>().text = string.Format("6 AM");
    }
}