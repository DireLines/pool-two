using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(TagHandler))]
public class BaseBall : MonoBehaviour {
    public int cost;

    [SerializeField]
    private Transform sprite;

    protected Rigidbody2D rb;

    bool moving;

    float epsilon = 0.1f;

    public virtual void OnHitByOtherBall() { }
    public virtual void OnHitOtherBall() { }
    protected virtual void OnMoving() { }
    protected virtual void OnSettle() { }
    public virtual void OnSink() { }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>().transform;
    }

    protected virtual void LateUpdate() {
        transform.right = Vector3.right;
        transform.up = Vector3.up;
    }

    protected virtual void Update() {
        if (rb.velocity.magnitude > epsilon) {
            moving = true;
            OnMoving();
        } else if (moving) {
            rb.velocity = Vector2.zero;
            moving = false;
            OnSettle();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (true) {//TODO: if other thing is ball
            if (!moving) {
                OnHitByOtherBall();
            } else {
                OnHitOtherBall();
            }
        }
    }
}
