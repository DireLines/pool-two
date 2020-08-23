using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Emotion {
    Idle,
    Mad,
    Sad,
    Happy,
}


public delegate void EmotiveEvent(int playerNyum);

public class EmotionListener : MonoBehaviour {
    //public event EmotiveEvent Emote;

    Animator anim;
    int playerNyum;

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        ScoreManager.OnHappy += OnHappy;
        ScoreManager.OnSadEvent += OnSad;
        var ball = GetComponent<BaseBall>();
        this.playerNyum = ball.ownerNumber;
    }

    public void OnHappy(int playerNum) {
        if (playerNum != this.playerNyum) return;
        anim.SetInteger("emotion", (int)Emotion.Happy);
    }

    public void OnMad() {
        anim.SetInteger("emotion", (int)Emotion.Mad);
    }

    public void OnSad(int playerNum) {
        if (playerNum != this.playerNyum) return;
        anim.SetInteger("emotion", (int)Emotion.Sad);
    }
}
