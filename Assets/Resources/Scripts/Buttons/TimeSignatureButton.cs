using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSignatureButton : MonoBehaviour, LeftClickable, Disableable {
    private float otherVerticalChange = 1.1f;
    private Sprite time24;
    private Sprite time32;
    private Sprite time36;
    private Sprite time48;

    private void Start() {
        time24 = Resources.Load<Sprite>("Sprites/Time Signature/24_time");
        time32 = Resources.Load<Sprite>("Sprites/Time Signature/32_time");
        time36 = Resources.Load<Sprite>("Sprites/Time Signature/36_time");
        time48 = Resources.Load<Sprite>("Sprites/Time Signature/48_time");

        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    private void arrangeOthers() {
        transform.GetChild(0).localPosition = new Vector3(0, otherVerticalChange, 0);
        transform.GetChild(1).localPosition = new Vector3(0, 2 * otherVerticalChange, 0);
        transform.GetChild(2).localPosition = new Vector3(0, 3 * otherVerticalChange, 0);

        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;

        transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
        transform.GetChild(1).GetComponent<Collider2D>().enabled = true;
        transform.GetChild(2).GetComponent<Collider2D>().enabled = true;
    }

    private void released() {
        transform.GetChild(0).localPosition = new Vector3();
        transform.GetChild(1).localPosition = new Vector3();
        transform.GetChild(2).localPosition = new Vector3();

        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;

        transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
        transform.GetChild(1).GetComponent<Collider2D>().enabled = false;
        transform.GetChild(2).GetComponent<Collider2D>().enabled = false;
    }

    public void leftClicked() {
        GameEvents.mouseUp[0].AddListener(released);
        arrangeOthers();
    }

    public void changeTimeSignature(Sprite highlightedSprite) {
        GameEvents.unfilledVoiceExists.Invoke();

        if (highlightedSprite == time24) {
            TimeSignature.changeTimeSignature(24);
            GetComponent<SpriteRenderer>().sprite = highlightedSprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = time32;
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = time36;
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = time48;
        }
        else if (highlightedSprite == time32) {
            TimeSignature.changeTimeSignature(32);
            GetComponent<SpriteRenderer>().sprite = highlightedSprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = time24;
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = time36;
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = time48;
        }
        else if (highlightedSprite == time36) {
            TimeSignature.changeTimeSignature(36);
            GetComponent<SpriteRenderer>().sprite = highlightedSprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = time24;
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = time32;
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = time48;
        }
        else if (highlightedSprite == time48) {
            TimeSignature.changeTimeSignature(48);
            GetComponent<SpriteRenderer>().sprite = highlightedSprite;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = time24;
            transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = time32;
            transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = time36;
        }
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
