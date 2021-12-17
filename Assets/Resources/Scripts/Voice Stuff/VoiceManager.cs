using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoiceManager {
    private static bool[] voiceOnArray =  new bool[4];
    private static GameObject[] voiceGameObjects = new GameObject[4];
    private static GameObject highlightedVoice;

    static VoiceManager() {
        voiceOnArray[0] = true;
    }

    public static void voiceTurnedOn(int i, GameObject newVoice) {
        voiceOnArray[i] = true;
        voiceGameObjects[i] = newVoice;
    }

    public static void voiceTurnedOff(int i) {
        voiceOnArray[i] = false;
    }

    public static void setHighlight(GameObject voice) {
        if (highlightedVoice != null) highlightedVoice.GetComponent<Voice>().destroyHighlight();
        highlightedVoice = voice;
    }

    public static GameObject getHighlight() {
        return highlightedVoice;
    }

    public static List<GameObject> getOnVoices() {
        List<GameObject> onVoices = new List<GameObject>(4);
        for (int i = 0; i < 4; i++) {
            if (voiceOnArray[i]) {
                onVoices.Add(voiceGameObjects[i]);
            }
        }
        return onVoices;
    }
}
