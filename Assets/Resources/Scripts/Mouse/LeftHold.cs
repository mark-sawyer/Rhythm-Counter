using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHold : MonoBehaviour {
    private void Update() {
        if (Input.GetMouseButtonUp(0)) {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void changeColour(Color newColour) {
        GetComponent<SpriteRenderer>().color = newColour;
    }
}
