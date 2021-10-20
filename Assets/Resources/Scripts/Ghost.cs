using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {
    [SerializeField] private float size;
    [SerializeField] private bool isTriplet;
    [SerializeField] private bool isNote;
    private AddBox addBox;

    private void Start() {
        adjustAlpha(0.5f);
        GameEvents.mouseUp[0].AddListener(mouseReleased);
    }

    void Update() {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.zero);
        Collider2D collider = ray.collider;
        if (collider is null || collider.GetComponent<AddBox>() is null) {
            adjustAlpha(0.5f);
            addBox = null;
        }
        else {
            addBox = collider.GetComponent<AddBox>();
            if (addBox.hasSpace(size)) {
                adjustAlpha(1);
            }
        }
    }

    private void adjustAlpha(float alphaValue) {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Color tempColour = sprite.color;
        tempColour.a = alphaValue;
        sprite.color = tempColour;
    }

    private void mouseReleased() {
        if (addBox != null) {
            if (!isTriplet) {
                if (addBox.hasSpace(size)) {
                    addBox.addToVoice(size, isNote);
                }
            }
            else {
                addBox.addTripletToVoice(isNote);
            }
        }
        Destroy(gameObject);
    }
}
