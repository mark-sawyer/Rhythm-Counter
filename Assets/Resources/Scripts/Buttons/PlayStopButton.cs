using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStopButton : MonoBehaviour, LeftClickable {
    private Sprite playSprite;
    private Sprite stopSprite;
    private bool playing;

    private void Start() {
        playSprite = Resources.Load<Sprite>("Sprites/play");
        stopSprite = Resources.Load<Sprite>("Sprites/stop");
    }

    public void leftClicked() {
        playing = !playing;
        if (playing) {
            GameEvents.playStarted.Invoke();
            GetComponent<SpriteRenderer>().sprite = stopSprite;
            GetComponent<PolygonCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = true;
            PlayManager.play();
        }
        else {
            GameEvents.playStopped.Invoke();
            GetComponent<SpriteRenderer>().sprite = playSprite;
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            PlayManager.stop();
        }
    }
}
