using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceRestFill : VoiceFill {
    public override void adjustSizeAndShape(float availableSpace, float requestedSpace, float length) {
        float totalSpace = TimeSignature.getTotalSpace();
        float unit = length / totalSpace;
        float xPos = (totalSpace - (availableSpace + requestedSpace)) * unit;
        float xScale = requestedSpace * unit;
        transform.localPosition = new Vector3(xPos, 0, 0);
        Vector2 startingPosition = transform.position;

        List<GameObject> leftSiblings = getLeftSiblings(startingPosition);

        foreach (GameObject sibling in leftSiblings) {
            if (sibling != gameObject) {
                xScale += sibling.transform.localScale.x;
                xPos -= sibling.transform.localScale.x;
            }
        }

        transform.localScale = new Vector3(xScale, 1, 1);
        transform.localPosition = new Vector3(xPos, 0, -1);

        foreach (GameObject sibling in leftSiblings) {
            if (sibling != gameObject) {
                Destroy(sibling);
            }
        }

        GetComponent<BoxCollider2D>().enabled = true;
        addBorders();
    }

    private List<GameObject> getLeftSiblings(Vector2 initialPosition) {
        List<GameObject> leftSiblings = new List<GameObject>();
        leftSiblings.Add(gameObject);
        Vector2 position = initialPosition + new Vector2(-0.001f, -0.5f);
        GameObject sibling;
        float siblingSize;
        RaycastHit2D ray;

        ray = Physics2D.Raycast(position, Vector2.zero, 0);
        while (ray.collider != null && ray.collider.GetComponent<VoiceRestFill>() != null) {
            sibling = ray.collider.gameObject;
            leftSiblings.Add(sibling);
            siblingSize = sibling.transform.localScale.x;
            position += new Vector2(-siblingSize, 0);
            ray = Physics2D.Raycast(position, Vector2.zero, 0);
        }

        return leftSiblings;
    }
}
