using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetButton : MonoBehaviour, LeftClickable, Disableable {
    public UnityEvent resetClicked = new UnityEvent();

    private void Start() {
        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    public void leftClicked() {
        resetClicked.Invoke();
        GameEvents.unfilledVoiceExists.Invoke();
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
