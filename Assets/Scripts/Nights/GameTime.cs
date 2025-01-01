using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour {
    public TMPro.TMP_Text text;
    [System.NonSerialized]
    public int time = 0;

    [System.NonSerialized]
    public int currentNight;

    private SixAM sixAmScript;

    void Awake() {
        currentNight = SceneManager.GetActiveScene().buildIndex - 1;
    }

    void Start() {
        text = GetComponent<TMPro.TMP_Text>();
        sixAmScript = GetComponent<SixAM>();

        text.text = "12 AM";
        StartCoroutine(timeIncrease());
    }

    void Update() {

        switch(time) {
            case 86:
                text.text = "1 AM";
                break;

            case 86 * 2:
                text.text = "2 AM";
                break;
            
            case 86 * 3:
                text.text = "3 AM";
                break;
            
            case 86 * 4:
                text.text = "4 AM";
                break;

            case 86 * 5:
                text.text = "5 AM";
                break;

            case 86 * 6:
                sixAmScript.EndNight();
                break;

            default:
                break;
        }
    }

    public static string ConvertTimeToHours(int ms, int hourLength) {
        if (ms < hourLength) {
            return "12 AM";
        } else if (hourLength <= ms && ms < hourLength * 2) {
            return "1 AM";
        } else if (hourLength * 2 <= ms && ms < hourLength * 3) {
            return "2 AM";
        } else if (hourLength * 3 <= ms && ms < hourLength * 4) {
            return "3 AM";
        } else if (hourLength * 4 <= ms && ms < hourLength * 5) {
            return "4 AM";
        } else if (hourLength * 5 <= ms && ms < hourLength * 6) {
            return "5 AM";
        } else if (hourLength * 6 <= ms) {
            return "6 AM";
        } else {
            return "";
        }
    }

    // Akorát počítá vteřiny
    IEnumerator timeIncrease() {
        while (true) {
            yield return new WaitForSeconds(1);
            time++;
            Debug.Log(string.Format("Time (ms): {0}", time));
        }
    } 
}
