using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripletAdder {
    private int currentThirds;

    public void addThird() {
        currentThirds += 1;
        if (currentThirds == 3) currentThirds = 0;
    }

    public void resetThirds() {
        currentThirds = 0;
    }

    public float getSize() {
        if (currentThirds <= 1) {
            return 87381f / 32768f;
        }
        else {
            return 87382f / 32768f;
        }
    }
}
