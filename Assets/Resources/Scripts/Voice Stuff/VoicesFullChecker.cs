using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoicesFullChecker {
    private static GameObject addBoxParent = GameObject.Find("add_boxes");
    private static GameObject playStop = GameObject.Find("play_stop");
    private static PlayableChecker playableChecker1;
    private static PlayableChecker playableChecker2;
    private static PlayableChecker playableChecker3;
    private static PlayableChecker playableChecker4;

    static VoicesFullChecker() {
        GameEvents.unfilledVoiceExists.AddListener(turnOffPlayButton);
    }

    public static void checkIfVoicesAreFull() {
        playableChecker1 = addBoxParent.transform.GetChild(0).GetComponent<PlayableChecker>();
        playableChecker2 = addBoxParent.transform.GetChild(1).GetComponent<PlayableChecker>();
        playableChecker3 = addBoxParent.transform.GetChild(2).GetComponent<PlayableChecker>();
        playableChecker4 = addBoxParent.transform.GetChild(3).GetComponent<PlayableChecker>();

        bool check1 = playableChecker1.isPlayable();
        bool check2 = playableChecker2.isPlayable();
        bool check3 = playableChecker3.isPlayable();
        bool check4 = playableChecker4.isPlayable();

        if (check1 && check2 && check3 && check4) {
            turnOnPlayButton();
        }
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
