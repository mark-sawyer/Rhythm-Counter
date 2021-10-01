using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeSignature {
    private static float totalSpace = 32;
    private static float speed = 60;

    public static float getTotalSpace() {
        return totalSpace;
    }

    public static void changeTimeSignature(float newSpace) {
        totalSpace = newSpace;
        GameEvents.timeSignatureChanged.Invoke();
    }

    public static float getSpeed() {
        return speed;
    }

    public static void changeSpeed(float newSpeed) {
        speed = newSpeed;
    }
}
