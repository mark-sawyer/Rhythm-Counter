using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceToggleButton : MonoBehaviour, LeftClickable, Disableable {
    [SerializeField] private bool voiceOn;
    [SerializeField] private int voiceIndex;
    private GameObject voiceRectangle;

    private void Start() {
        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
        if (voiceOn) {
            turnOnVoice();
            transform.GetChild(0).GetComponent<Voice>().highlight();
        }
        else GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/subtract_button");
    }

    public void leftClicked() {
        voiceOn = !voiceOn;
        swapSprite();
        if (voiceOn) turnOnVoice();
        else turnOffVoice();
        VoicesFullChecker.checkIfVoicesAreFull();
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }

    private void swapSprite() {
        if (voiceOn) GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/add_button");
        else GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/subtract_button");
    }

    private void turnOnVoice() {
        GameObject voiceRectanglePrefab = Resources.Load<GameObject>("Prefabs/Voice/voice_rectangle");
        voiceRectangle = Instantiate(voiceRectanglePrefab, transform);
        voiceRectangle.GetComponent<Voice>().setupVoice(voiceIndex);
        voiceRectangle.transform.localPosition = new Vector3(-17, 0, 0);
        voiceRectangle.GetComponent<Voice>().highlight();
    }

    private void turnOffVoice() {
        voiceRectangle.GetComponent<Voice>().removeVoice();
        Destroy(voiceRectangle);
    }
}
