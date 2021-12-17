using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButton : MonoBehaviour, LeftClickable, Disableable {
    [SerializeField] private float size;
    [SerializeField] private bool isTriplet;
    [SerializeField] private bool isNote;

    private void Start() {
        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    public void leftClicked() {
        GameObject highlightedVoiceGameObject = VoiceManager.getHighlight();
        if (highlightedVoiceGameObject != null) {
            Voice highlightedVoice = highlightedVoiceGameObject.GetComponent<Voice>();
            if (!isTriplet) {
                if (highlightedVoice.hasSpace(size)) {
                    highlightedVoice.addToVoice(size, isNote);
                }
            }
            else {
                highlightedVoice.addTripletToVoice(isNote);
            }
        }
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
