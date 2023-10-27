using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour {   
    public Newspaper paperscript;
    
    public void OnButtonPress() {
        PlayerPrefs.SetInt("night", 1);
        paperscript.gameObject.SetActive(true);
        paperscript.ShowPaper();
    }
}
