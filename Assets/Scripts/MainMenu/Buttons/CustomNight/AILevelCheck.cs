using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AILevelCheck : MonoBehaviour {
    public TMP_InputField text;

    [System.NonSerialized]
    public int chosenAILevel;

    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TMP_InputField>();
    }

    public void OnTextBoxLeave() {
        if (int.TryParse(text.text, out int j)) {
            if (j > 20) {
                text.text = "20";
            }
        } else {
            text.text = "1";
        }

        chosenAILevel = int.Parse(text.text);
    }
}
