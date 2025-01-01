using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour {
    public TMPro.TMP_Text text;
    [System.NonSerialized]
    public int time = 0;

    [System.NonSerialized]
    public int currentNight;

    int hour1 = 86;
    int hour2;
    int hour3;
    int hour4;
    int hour5;

    private SixAM sixAmScript;

    void Awake() {
        currentNight = SceneManager.GetActiveScene().buildIndex - 1;
    }

    void Start() {
        text = GetComponent<TMPro.TMP_Text>();
        sixAmScript = GetComponent<SixAM>();
        
        hour2 = hour1 * 2;
        hour3 = hour1 * 3;
        hour4 = hour1 * 4;
        hour5 = hour1 * 5;

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

    public static string ConvertTimeToHours(int ms) {
        return ms switch
        {
            <86 => "12 AM",
            86 => "1 AM",
            86 * 2 => "2 AM",
            86 * 3 => "3 AM",
            86 * 4 => "4 AM",
            86 * 5 => "5 AM",
            86 * 6 => "6 AM",
            _ => "",
        };
    }

    // Akorát počítá vteřiny
    IEnumerator timeIncrease() {
        while (true) {
            yield return new WaitForSeconds(1);
            time++;
        }
    } 
}
