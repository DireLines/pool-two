using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(TagHandler))]
public class BaseBall : MonoBehaviour {
    public int cost;

    protected SpriteRenderer icon;
    protected SpriteRenderer inside;
    protected SpriteRenderer outline;

    protected Rigidbody2D rb;

    bool moving;

    float epsilon = 0.1f;

    protected virtual void OnHitByOtherBall() { }
    protected virtual void OnHitOtherBall() { }
    protected virtual void OnMoving() { }
    protected virtual void OnSettle() { }
    public virtual void OnSink() { }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();

        icon = transform.Find("Icon")?.GetComponent<SpriteRenderer>();
        inside = transform.Find("Inside")?.GetComponent<SpriteRenderer>();
        outline = transform.Find("Outline")?.GetComponent<SpriteRenderer>();

        TagHandler handler = GetComponent<TagHandler>();
        if (!handler) handler = gameObject.AddComponent<TagHandler>();
        handler.tags.Add(Tag.Ball);
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
        if (collision.gameObject.HasTag(Tag.Ball)) {
            if (!moving) {
                OnHitByOtherBall();
            } else {
                OnHitOtherBall();
            }
        }
    }
}
