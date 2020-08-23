using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Emotion {
    Idle,
    Mad,
    Sad,
    Had,
}

//public delegate void EmotiveEvent(Emotion emotion);

public class EmotionListener : MonoBehaviour {
    //public event EmotiveEvent Emote;

    Animator anim;

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        //Emote += OnListen;
    }

    public void OnListen(Emotion emotion) {
        anim.SetInteger("emotion", (int)emotion);
    }
}
