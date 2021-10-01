using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour {
    private GameObject voiceFill;
    private GameObject restFill;
    private static float length = 16;

    private void Start() {
        voiceFill = Resources.Load<GameObject>("Prefabs/Voice/voice_fill");
        restFill = Resources.Load<GameObject>("Prefabs/Voice/voice_rest_fill");
    }

    public void addFill(float availableSpace, float requestedSpace, bool isNote) {
        GameObject newVoiceFill;
        if (isNote) newVoiceFill = Instantiate(voiceFill, transform);
        else newVoiceFill = Instantiate(restFill, transform);

        newVoiceFill.GetComponent<VoiceFill>().adjustSizeAndShape(availableSpace, requestedSpace, length);
    }

    public void removeFills() {
        int children = transform.childCount;

        for (int i = 0; i < children; i++) {
            Transform childTransform = transform.GetChild(i);
            Destroy(childTransform.gameObject);
        }        
    }

    public static float getLength() {
        return length;
    }

    public void destroyPlaySlider() {
        int totalChildren = transform.childCount;
        GameObject slider = transform.GetChild(totalChildren - 1).gameObject;
        Destroy(slider);
    }
}
