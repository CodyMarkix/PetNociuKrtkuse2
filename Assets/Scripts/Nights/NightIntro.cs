using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightIntro : MonoBehaviour
{
    public GameObject nightTextObj;
    public GameObject tabletScreen;
    private Animator animator;  
    private TMPro.TMP_Text nightText;
    private int nightToShow = 1;

    void Awake() {
        animator = nightTextObj.GetComponent<Animator>();
        nightText = nightTextObj.GetComponent<TMPro.TMP_Text>();
    }

    void Start() {
        nightToShow = SceneManager.GetActiveScene().buildIndex - 1;        
        nightText.text = string.Format("Noc {0}\n12:00 AM", nightToShow);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(showAnimation());
    }

    IEnumerator showAnimation() {
        animator.SetBool("seenIntro", true);
        yield return new WaitForSeconds(2);
        animator.SetBool("seenIntro", false);
    }
}
