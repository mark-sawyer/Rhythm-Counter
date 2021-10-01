using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySlider : MonoBehaviour {
    private GameObject voiceFill;
    private Vector3 speed;

    private void Start() {
        float beatsPerMinute = TimeSignature.getSpeed();
        float secondsPerBeat = 60 / beatsPerMinute;
        float notesPerBar = TimeSignature.getTotalSpace() / 8;
        float lengthPerNote = Voice.getLength() / notesPerBar;
        float xSpeed = lengthPerNote / secondsPerBeat;
        speed = new Vector3(xSpeed, 0, 0);
        setCorrectSound();
    }

    private void Update() {
        transform.position += (Time.deltaTime * speed);
        if (transform.localPosition.x >= Voice.getLength()) {
            transform.localPosition = new Vector3();
        }

        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.zero);
        GameObject voiceFillOver = ray.collider.gameObject;
        if (voiceFill != voiceFillOver) {
            voiceFill = voiceFillOver;
            if (voiceFill.GetComponent<VoiceNoteFill>() != null) {
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void setCorrectSound() {
        float yPosition = transform.position.y;
        switch (yPosition) {
            case 3.5f:
                GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/tick");
                break;
            case 1.5f:
                GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/thud");
                break;
            case -0.5f:
                GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/pop");
                break;
            case -2.5f:
                GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/knock");
                break;
        }
    }
}
