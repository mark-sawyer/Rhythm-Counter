using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffAddBox : MonoBehaviour, LeftClickable, PlayableChecker, Disableable {
    private bool removable = true;

    private void Start() {
        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    public void leftClicked() {
        GameEvents.unfilledVoiceExists.Invoke();
        GameObject addBoxPrefab = Resources.Load<GameObject>("Prefabs/addbox");
        GameObject newAddBox = Instantiate(addBoxPrefab, transform.position, Quaternion.identity);
        newAddBox.GetComponent<AddBox>().setRemoveable(removable);
        Transform addBoxParent = GameObject.Find("add_boxes").transform;
        newAddBox.transform.SetParent(addBoxParent);
        Destroy(gameObject);
    }

    public bool isPlayable() {
        return true;
    }

    public void disableOnPlay() {
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void enableOnStop() {
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
