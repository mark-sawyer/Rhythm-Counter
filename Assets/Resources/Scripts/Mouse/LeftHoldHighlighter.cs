using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHoldHighlighter {
    private GameObject currentLeftHoldHighlight;

    public LeftHoldHighlighter() {
        GameEvents.mouseUp[0].AddListener(checkForTimeSignatureChange);
    }

    public void manageHighlighting(RaycastHit2D ray) {
        Collider2D collider = ray.collider;
        if (collider != null) {
            giveHighlight(collider.gameObject);
        }
        else {
            resetHighlight();
        }
    }

    private void giveHighlight(GameObject highlightableGameObject) {
        highlightableGameObject.GetComponent<LeftHold>().changeColour(Color.yellow);
        if (highlightableGameObject == currentLeftHoldHighlight) {
            return;
        }
        if (currentLeftHoldHighlight == null) {
            currentLeftHoldHighlight = highlightableGameObject;
            return;
        }
        currentLeftHoldHighlight.GetComponent<LeftHold>().changeColour(Color.white);
        currentLeftHoldHighlight = highlightableGameObject;
    }

    private void resetHighlight() {
        if (currentLeftHoldHighlight != null) {
            currentLeftHoldHighlight.GetComponent<LeftHold>().changeColour(Color.white);
            currentLeftHoldHighlight = null;
        }
    }

    private void checkForTimeSignatureChange() {
        if (currentLeftHoldHighlight == null) return;

        TimeSignatureButton timeSignatureButton;
        if (currentLeftHoldHighlight.GetComponent<TimeSignatureButton>() != null) {
            timeSignatureButton = currentLeftHoldHighlight.GetComponent<TimeSignatureButton>();
        }
        else {
            timeSignatureButton = currentLeftHoldHighlight.transform.parent.GetComponent<TimeSignatureButton>();
        }

        timeSignatureButton.changeTimeSignature(currentLeftHoldHighlight.GetComponent<SpriteRenderer>().sprite);
    }
}
