using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(AudioSource))]
public class PositionCheck : MonoBehaviour, IPointerEnterHandler
{
    public GameObject chevron;
    new AudioSource audio;
    private Vector3 offset = new Vector3(10, 0, 0);

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData) {

        switch(gameObject.name) {
            case "NewGame":
                chevron.transform.position = gameObject.transform.position + offset;
                audio.Play(0);
                break;
            
            case "Continue":
                chevron.transform.position = gameObject.transform.position + offset;
                audio.Play(0);
                break;

            case "NightSix":
                chevron.transform.position = gameObject.transform.position + offset;
                audio.Play(0);
                break;

            case "NightSeven":
                chevron.transform.position = gameObject.transform.position + offset;
                audio.Play(0);
                break;
            
            case "QuitGame":
                chevron.transform.position = gameObject.transform.position + offset;
                audio.Play(0);
                break;
        }
    }
    
}
