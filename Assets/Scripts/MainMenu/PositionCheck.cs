using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Timeline;

[RequireComponent(typeof(AudioSource))]
public class PositionCheck : MonoBehaviour, IPointerEnterHandler {
    public GameObject chevron;
    new AudioSource audio;
    private Vector3 offset = new Vector3(10, 0, 0);

    void Start() {
        audio = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        chevron.transform.position = gameObject.transform.position + offset;
        audio.Play(0);
    }    
}
