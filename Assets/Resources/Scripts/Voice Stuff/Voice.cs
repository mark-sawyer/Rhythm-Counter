using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voice : MonoBehaviour, LeftClickable {
    [SerializeField] private float totalSpace;
    private float availableSpace;
    private GameObject voiceFill;
    private GameObject restFill;
    private static float length = 16;
    private TripletAdder tripletAdder;
    private int voiceIndex;
    private float selectionPositionOffset = 0.15625f;

    private void Start() {
        voiceFill = Resources.Load<GameObject>("Prefabs/Voice/voice_fill");
        restFill = Resources.Load<GameObject>("Prefabs/Voice/voice_rest_fill");

        tripletAdder = new TripletAdder();

        GameEvents.timeSignatureChanged.AddListener(reset);
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
            if (childTransform.GetComponent<VoiceFill>() != null) {
                Destroy(childTransform.gameObject);
            }
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

    private void reset() {
        totalSpace = TimeSignature.getTotalSpace();
        availableSpace = totalSpace;
        removeFills();
        tripletAdder.resetThirds();
    }

    public bool hasSpace(float requestedSpace) {
        return requestedSpace <= availableSpace;
    }

    public void addTripletToVoice(bool isNote) {
        float size = tripletAdder.getSize();
        if (hasSpace(size)) {
            addToVoice(size, isNote);
            tripletAdder.addThird();
        }
    }

    public bool isPlayable() {
        return availableSpace == 0;
    }

    public void addToVoice(float requestedSpace, bool isNote) {
        availableSpace -= requestedSpace;
        addFill(availableSpace, requestedSpace, isNote);
        VoicesFullChecker.checkIfVoicesAreFull();
    }

    public void setupVoice(int i) {
        voiceIndex = i;
        VoiceManager.voiceTurnedOn(voiceIndex, gameObject);
        totalSpace = TimeSignature.getTotalSpace();
        availableSpace = totalSpace;
    }

    public void removeVoice() {
        VoiceManager.voiceTurnedOff(voiceIndex);
    }

    public void leftClicked() {
        if (VoiceManager.getHighlight() != gameObject) {
            highlight();
        }
    }

    public void highlight() {
        GameObject voiceSelectionIndicatorPrefab = Resources.Load<GameObject>("Prefabs/voice_selection_indicator");
        GameObject voiceSelectionIndicator = Instantiate(voiceSelectionIndicatorPrefab);
        voiceSelectionIndicator.transform.parent = transform;
        voiceSelectionIndicator.transform.localPosition = new Vector3(-selectionPositionOffset, selectionPositionOffset);
        VoiceManager.setHighlight(gameObject);
    }

    public void destroyHighlight() {
        Destroy(transform.GetChild(0).gameObject);
    }
}
