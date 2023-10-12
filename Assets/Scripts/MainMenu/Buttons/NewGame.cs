using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGame : MonoBehaviour {   
    public Newspaper paperscript;
    
    public void OnButtonPress() {
        paperscript.gameObject.SetActive(true);
        paperscript.ShowPaper();
    }
}
