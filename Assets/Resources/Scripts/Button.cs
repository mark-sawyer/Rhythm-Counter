using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, LeftClickable, Disableable {
    [SerializeField] private GameObject ghost;

    private void Start() {
        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    public void leftClicked() {
        Instantiate(ghost, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
