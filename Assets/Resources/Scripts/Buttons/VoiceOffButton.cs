using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOffButton : MonoBehaviour, LeftClickable, Disableable {
    private void Start() {
        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    public void leftClicked() {
        GameObject addBoxParent = transform.parent.gameObject;
        GameObject offAddBox = Resources.Load<GameObject>("Prefabs/off_addbox");
        GameObject newOffAddBox = Instantiate(offAddBox, addBoxParent.transform.position, Quaternion.identity);
        newOffAddBox.transform.parent = GameObject.Find("add_boxes").transform;
        addBoxParent.transform.parent = null;
        VoicesFullChecker.checkIfVoicesAreFull();
        Destroy(addBoxParent);
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
