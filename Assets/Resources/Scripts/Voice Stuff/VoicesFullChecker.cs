using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoicesFullChecker {
    private static GameObject playStop = GameObject.Find("play_stop");

    static VoicesFullChecker() {
        GameEvents.unfilledVoiceExists.AddListener(turnOffPlayButton);
    }

    public static void checkIfVoicesAreFull() {
        List<GameObject> onVoices = VoiceManager.getOnVoices();
        bool noUnfilledVoices = true;
        bool atLeastOneVoice = onVoices.Count != 0;
        foreach (GameObject voice in onVoices) {
            if (!voice.GetComponent<Voice>().isPlayable()) {
                noUnfilledVoices = false;
                break;
            }
        }

        if (noUnfilledVoices && atLeastOneVoice) turnOnPlayButton();
        else turnOffPlayButton();
    }

    private static void turnOnPlayButton() {
        Color tempColour = playStop.GetComponent<SpriteRenderer>().color;
        tempColour.a = 1;
        playStop.GetComponent<SpriteRenderer>().color = tempColour;
        playStop.GetComponent<PolygonCollider2D>().enabled = true;
    }

    private static void turnOffPlayButton() {
        Color tempColour = playStop.GetComponent<SpriteRenderer>().color;
        tempColour.a = 0.5f;
        playStop.GetComponent<SpriteRenderer>().color = tempColour;
        playStop.GetComponent<PolygonCollider2D>().enabled = false;
    }
}
