using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Newspaper : MonoBehaviour {
    public GameObject mainmenu;
    public Animator animator;

    public void ShowPaper() {
        StartCoroutine(showNewspapers());
    }

    IEnumerator hideNewspapers() {   
        animator.SetBool("Show", false);
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(2);
    }
    
    IEnumerator showNewspapers() {
        mainmenu.SetActive(false);
        animator.SetBool("Show", true);
        yield return new WaitForSeconds(5);

        StartCoroutine(hideNewspapers());
    }

}
