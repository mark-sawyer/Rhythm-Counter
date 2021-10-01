using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class GameEvents {
    public static UnityEvent[] mouseUp = { new UnityEvent(), new UnityEvent() };
    public static UnityEvent timeSignatureChanged = new UnityEvent();
    public static UnityEvent unfilledVoiceExists = new UnityEvent();
    public static UnityEvent playStarted = new UnityEvent();
    public static UnityEvent playStopped = new UnityEvent();
}
