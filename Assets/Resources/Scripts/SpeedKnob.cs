using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedKnob : MonoBehaviour, LeftClickable, Disableable {
    private bool moving;

    private void Start() {
        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            moving = false;
            float localX = transform.localPosition.x;
            float newSpeed = translateXToSpeed(localX);
            TimeSignature.changeSpeed(newSpeed);
            return;
        }
        if (moving) {
            float mouseX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            transform.position = new Vector3(mouseX, transform.position.y, 0);
            if (transform.localPosition.x < 0.1f) {
                transform.localPosition = new Vector3(0.1f, transform.localPosition.y, 0);
            }
            else if (transform.localPosition.x > 7.9f) {
                transform.localPosition = new Vector3(7.9f, transform.localPosition.y, 0);
            }
        }
    }

    private float translateXToSpeed(float localX) {
        return ((850 / 39) * localX + (1085 / 39));
    }

    public void leftClicked() {
        moving = true;
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
