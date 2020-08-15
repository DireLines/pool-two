using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBall : MonoBehaviour {
    protected Transform body, sprite;

    public int cost;

    public UnityEvent OnLaunch;
    public UnityEvent OnCollision;
    public UnityEvent OnSettle;
    public UnityEvent OnSink;

    protected virtual void Start() {
        body = transform.FindDeepChild("Body");
        sprite = transform.FindDeepChild("Sprite");
    }

    protected virtual void LateUpdate() {
        sprite.position = body.position;
    }
}
