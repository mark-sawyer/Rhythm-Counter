using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour {
    private LeftHoldHighlighter leftHoldHighlighter = new LeftHoldHighlighter();
    private LayerMask layerMask;
    private bool[] down = { false, false };

    private void Start() {        
        layerMask = LayerMask.GetMask("time_signature");
    }

    private void Update() {
        if (!down[0] && !down[1]) {
            detectClick(0);
            if (!down[0]) detectClick(1);
        }
        else detectRelease();
        if (down[0]) leftHold();
    }

    private void detectClick(int i) {
        if (Input.GetMouseButtonDown(i)) {
            down[i] = true;
            clickMousedOverObject(i);
        }
    }

    private void clickMousedOverObject(int i) {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero);
        Collider2D collider = ray.collider;
        if (collider != null) {
            if (i == 0 && collider.GetComponent<LeftClickable>() != null) {
                ray.collider.GetComponent<LeftClickable>().leftClicked();
            }
            if (i == 1 && collider.GetComponent<RightClickable>() != null) {
                ray.collider.GetComponent<RightClickable>().rightClicked();
            }
        }
    }

    private void leftHold() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, layerMask);
        leftHoldHighlighter.manageHighlighting(ray);
    }

    private void detectRelease() {
        int i = down[0] ? 0 : 1;
        if (Input.GetMouseButtonUp(i)) {
            down[i] = false;
            GameEvents.mouseUp[i].Invoke();
        }
    }
}
