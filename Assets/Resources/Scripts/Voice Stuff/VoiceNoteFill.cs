using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceNoteFill : VoiceFill, RightClickable, Disableable {
    private LayerMask layerMask;
    private Transform parentTransform;
    private Color highlightColour;
    private Color regularColour;
    private Vector2 clickPosition;
    private bool tying = false;
    private bool playing = false;

    private void Start() {
        parentTransform = transform.parent;
        layerMask = LayerMask.GetMask("voice_fill");
        highlightColour = Color.yellow;
        regularColour = Color.white;

        GameEvents.playStarted.AddListener(disableOnPlay);
        GameEvents.playStopped.AddListener(enableOnStop);
    }

    private void Update() {
        if (tying) {
            createRay();
        }
    }

    private void createRay() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (!mousedOverFamily(mousePos)) {
            unhighlightSiblings();
            return;
        }

        Vector2 direction = mousePos - clickPosition;
        float magnitude = direction.magnitude;
        RaycastHit2D[] rayHits = Physics2D.RaycastAll(clickPosition, direction, magnitude, layerMask);

        highlightSiblings(rayHits);
    }

    private bool mousedOverFamily(Vector2 mousePos) {
        RaycastHit2D ray = Physics2D.Raycast(mousePos, Vector2.zero, 0, layerMask);
        return ray.collider != null && ray.collider.transform.parent == parentTransform;
    }

    private void unhighlightSiblings() {
        int familyCount = parentTransform.childCount;
        for (int i = 0; i < familyCount; i++) {
            GameObject sibling = parentTransform.GetChild(i).gameObject;
            if (sibling != gameObject && sibling.GetComponent<VoiceNoteFill>() != null) {
                sibling.GetComponent<VoiceNoteFill>().changeColour(regularColour);
            }
        }
    }

    private void highlightSiblings(RaycastHit2D[] rayHits) {
        List<GameObject> siblings = getAdjacentSiblings();
        foreach (GameObject sibling in siblings) {
            bool highlighted = false;
            foreach (RaycastHit2D rayHit in rayHits) {
                if (sibling == rayHit.collider.gameObject) {
                    sibling.GetComponent<VoiceNoteFill>().changeColour(highlightColour);
                    highlighted = true;
                    break;
                }
            }
            if (!highlighted) sibling.GetComponent<VoiceNoteFill>().changeColour(regularColour);
        }
    }

    private List<GameObject> getAdjacentSiblings() {
        List<GameObject> adjacentSiblings = new List<GameObject>();
        adjacentSiblings.Add(gameObject);
        Vector2 position = transform.position + new Vector3(-0.001f, -0.5f, 0);
        GameObject sibling;
        float siblingSize;
        RaycastHit2D ray;

        // Left
        ray = Physics2D.Raycast(position, Vector2.zero, 0, layerMask);
        while (ray.collider != null) {
            sibling = ray.collider.gameObject;
            adjacentSiblings.Add(sibling);
            siblingSize = sibling.transform.localScale.x;
            position += new Vector2(-siblingSize, 0);
            ray = Physics2D.Raycast(position, Vector2.zero, 0, layerMask);
        }

        // Right
        position = transform.position + new Vector3(transform.localScale.x + 0.001f, -0.5f, 0);
        ray = Physics2D.Raycast(position, Vector2.zero, 0, layerMask);
        while (ray.collider != null) {
            sibling = ray.collider.gameObject;
            adjacentSiblings.Add(sibling);
            siblingSize = sibling.transform.localScale.x;
            position += new Vector2(siblingSize, 0);
            ray = Physics2D.Raycast(position, Vector2.zero, 0, layerMask);
        }

        return adjacentSiblings;
    }

    private void changeColour(Color colour) {
        GetComponent<SpriteRenderer>().color = colour;
        transform.GetChild(0).GetComponent<SpriteRenderer>().color = colour;
        transform.GetChild(1).GetComponent<SpriteRenderer>().color = colour;
    }

    private void endTying() {
        int familyCount = parentTransform.childCount;
        float totalSize = 0;
        float startPosition = transform.localPosition.x;
        for (int i = 0; i < familyCount; i++) {
            GameObject sibling = parentTransform.GetChild(i).gameObject;
            Color siblingColour = sibling.GetComponent<SpriteRenderer>().color;
            if (siblingColour == highlightColour) {
                totalSize += sibling.transform.localScale.x;
                if (sibling.transform.localPosition.x < startPosition) {
                    startPosition = sibling.transform.localPosition.x;
                }
                if (sibling != gameObject) {
                    Destroy(sibling);
                }
            }
        }

        GameObject newVoiceFillPrefab = Resources.Load<GameObject>("Prefabs/Voice/voice_fill");
        GameObject newVoiceFill = Instantiate(newVoiceFillPrefab, parentTransform);
        newVoiceFill.transform.localPosition = new Vector3(startPosition, 0, 0);
        newVoiceFill.transform.localScale = new Vector3(totalSize, 1, 1);
        newVoiceFill.GetComponent<VoiceNoteFill>().addBorders();

        Destroy(gameObject);
    }

    public void rightClicked() {
        if (playing) return;

        tying = true;
        changeColour(highlightColour);
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameEvents.mouseUp[1].AddListener(endTying);
    }

    public override void adjustSizeAndShape(float availableSpace, float requestedSpace, float length) {
        float totalSpace = TimeSignature.getTotalSpace();
        float unit = length / totalSpace;
        float xScale = unit * requestedSpace;
        transform.localScale = new Vector3(xScale, 1, 1);

        float xPos = (totalSpace - (availableSpace + requestedSpace)) * unit;
        transform.localPosition = new Vector3(xPos, 0, -1);

        addBorders();
    }

    public void disableOnPlay() {
        playing = true;
    }

    public void enableOnStop() {
        playing = false;
    }
}
