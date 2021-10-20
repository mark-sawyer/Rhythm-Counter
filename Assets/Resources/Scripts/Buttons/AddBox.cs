using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBox : MonoBehaviour, PlayableChecker, Disableable {
    [SerializeField] private float totalSpace;
    [SerializeField] private bool removable;
    private float availableSpace;
    private GameObject resetButton;
    private GameObject offButton;
    private GameObject voiceRectangle;
    private TripletAdder tripletAdder;

    private void Start() {
        totalSpace = TimeSignature.getTotalSpace();
        GameEvents.timeSignatureChanged.AddListener(reset);
        availableSpace = totalSpace;

        GameObject resetButtonPrefab = Resources.Load<GameObject>("Prefabs/reset_button");
        resetButton = Instantiate(resetButtonPrefab, transform);
        resetButton.transform.localPosition = new Vector3(1.25f, 0, 0);
        resetButton.GetComponent<ResetButton>().resetClicked.AddListener(reset);

        GameObject voiceRectanglePrefab = Resources.Load<GameObject>("Prefabs/Voice/voice_rectangle");
        voiceRectangle = Instantiate(voiceRectanglePrefab, transform);
        voiceRectangle.transform.localPosition = new Vector3(-17, 0, 0);

        if (removable) {
            GameObject offButtonPrefab = Resources.Load<GameObject>("Prefabs/off_button");
            offButton = Instantiate(offButtonPrefab, transform);
            offButton.transform.localPosition = new Vector3(1.25f, -0.812f, 0);
        }

        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);

        tripletAdder = new TripletAdder();
    }

    private void reset() {
        totalSpace = TimeSignature.getTotalSpace();
        availableSpace = totalSpace;
        voiceRectangle.GetComponent<Voice>().removeFills();
        tripletAdder.resetThirds();
    }

    public bool hasSpace(float requestedSpace) {
        return requestedSpace <= availableSpace;
    }

    public void addToVoice(float requestedSpace, bool isNote) {
        availableSpace -= requestedSpace;
        voiceRectangle.GetComponent<Voice>().addFill(availableSpace, requestedSpace, isNote);
        VoicesFullChecker.checkIfVoicesAreFull();
    }

    public void addTripletToVoice(bool isNote) {
        float size = tripletAdder.getSize();
        if (hasSpace(size)) {
            addToVoice(size, isNote);
            tripletAdder.addThird();
        }
    }

    public void setRemoveable(bool b) {
        removable = b;
    }

    public bool isPlayable() {
        return availableSpace == 0;
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
