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
        int nextNight = SceneManager.GetActiveScene().buildIndex + 1;

        StartCoroutine(SetRandomNumbers());
        yield return new WaitForSeconds(10f);
        if (scene <= 5 && scene >= 2 ) {
            // Načte další noc
            PlayerPrefs.SetInt("night", nextNight);
            PlayerPrefs.Save();

            SceneManager.LoadScene(nextNight);
        } else if (scene == 6) {
            // Načte victory scénu
            PlayerPrefs.SetInt("night", nextNight);
            PlayerPrefs.Save();

            SceneManager.LoadScene(10);
        } else if (scene == 7 || scene == 8) {
            // Načte main menu
            if (scene == 7) {
                PlayerPrefs.SetInt("night", nextNight);
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