using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VoiceFill : MonoBehaviour {
    [SerializeField] private GameObject verticalBorder;

    public void addBorders() {
        float xScale = transform.localScale.x;

        GameObject leftBorder = Instantiate(verticalBorder, transform);
        leftBorder.transform.localScale = new Vector3(1 / xScale, 1, 1);
        leftBorder.transform.localPosition = new Vector3();

        GameObject rightBorder = Instantiate(verticalBorder, transform);
        rightBorder.transform.localScale = new Vector3(-1 / xScale, 1, 1);
        rightBorder.transform.localPosition = new Vector3(1, 0, 0);
    }

    public abstract void adjustSizeAndShape(float availableSpace, float requestedSpace, float length);
}
