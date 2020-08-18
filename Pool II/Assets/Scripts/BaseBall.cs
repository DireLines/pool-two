using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBall : MonoBehaviour {
    public int cost;

    [SerializeField]
    private Transform sprite;

    public virtual void OnFirstBallCollide() { }
    public virtual void OnBallCollide() { }
    public virtual void OnSettle() { }
    public virtual void OnSink() { }

    protected virtual void Start() {
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>().transform;
    }

    protected virtual void LateUpdate() {
        transform.right = Vector3.right;
        transform.up = Vector3.up;
    }
}
