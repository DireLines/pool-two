using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBall : MonoBehaviour {
    protected Transform body, sprite;

    public int cost;

    protected virtual void OnLaunch() { }
    protected virtual void OnSettle() { }
    protected virtual void OnSink() { }

    protected virtual void Start() {
        body = transform.FindDeepChild("Body");
        sprite = transform.FindDeepChild("Sprite");
    }

    protected virtual void LateUpdate() {
        sprite.position = body.position;
    }
}
