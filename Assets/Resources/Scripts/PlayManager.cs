using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayManager {
    private static GameObject sliderPrefab;
    private static Transform[] voiceTransforms = new Transform[4];
    private static bool[] voiceOn = new bool[3];

    static PlayManager() {
        sliderPrefab = Resources.Load<GameObject>("Prefabs/play_slider");
    }

    public static void play() {
        List<GameObject> onVoices = VoiceManager.getOnVoices();
        foreach (GameObject onVoice in onVoices) {
            GameObject.Instantiate(sliderPrefab, onVoice.transform);
        }
    }

    public static void stop() {
        List<GameObject> onVoices = VoiceManager.getOnVoices();
        foreach (GameObject onVoice in onVoices) {
            onVoice.GetComponent<Voice>().destroyPlaySlider();
        }
    }
}
