using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayManager {
    private static GameObject sliderPrefab;
    private static Transform addBoxTransform;
    private static Transform[] voiceTransforms = new Transform[4];
    private static bool[] voiceOn = new bool[3];

    static PlayManager() {
        sliderPrefab = Resources.Load<GameObject>("Prefabs/play_slider");
        addBoxTransform = GameObject.Find("add_boxes").transform;
        voiceTransforms[0] = addBoxTransform.GetChild(0).GetChild(1);
    }

    public static void play() {
        getWhichVoicesOn();
        createAppropriateSliders();
    }

    public static void stop() {
        for (int i = 0; i < 4; i++) {
            if (voiceTransforms[i] != null) {
                voiceTransforms[i].GetComponent<Voice>().destroyPlaySlider();
            }
        }
    }

    private static void getWhichVoicesOn() {
        checkAndGetVoices(addBoxTransform.GetChild(1));
        checkAndGetVoices(addBoxTransform.GetChild(2));
        checkAndGetVoices(addBoxTransform.GetChild(3));
    }

    private static void createAppropriateSliders() {
        GameObject.Instantiate(sliderPrefab, voiceTransforms[0]);

        for (int i = 0; i < 3; i++) {
            if (voiceOn[i]) {
                GameObject.Instantiate(sliderPrefab, voiceTransforms[i + 1]);
            }
        }
    }

    private static void checkAndGetVoices(Transform t) {
        bool b = t.GetComponent<AddBox>() != null;
        float yPos = t.localPosition.y;
        switch (yPos) {
            case -2:
                voiceOn[0] = b;
                if (b) {
                    voiceTransforms[1] = t.GetChild(1);
                }
                break;
            case -4:
                voiceOn[1] = b;
                if (b) {
                    voiceTransforms[2] = t.GetChild(1);
                }
                break;
            case -6:
                voiceOn[2] = b;
                if (b) {
                    voiceTransforms[3] = t.GetChild(1);
                }
                break;
        }
    }
}
