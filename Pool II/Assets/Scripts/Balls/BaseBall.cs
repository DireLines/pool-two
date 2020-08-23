﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(TagHandler))]
public class BaseBall : MonoBehaviour {
    public int cost;
    public int ownerNumber;// {get; private set; }

    protected SpriteRenderer icon;
    protected SpriteRenderer inside;
    protected SpriteRenderer outline;

    protected Rigidbody2D rb;

    public bool moving { get; private set; }

    public bool struckByBall { get; private set; }

    float epsilon = 0.1f;

    float timeMoving, dragThreshold = 5f, dragRate = 1f, originalDrag;

    protected virtual void OnHitByOtherBall() { }
    protected virtual void OnHitOtherBall() { }
    protected virtual void OnHitNotBall() { }
    protected virtual void OnMoving() { }
    protected virtual void OnSettle() { }
    public virtual void OnSink() { }

    protected virtual void Start() {
        rb = GetComponent<Rigidbody2D>();
        originalDrag = rb.drag;

        icon = transform.Find("Icon")?.GetComponent<SpriteRenderer>();
        inside = transform.Find("Inside")?.GetComponent<SpriteRenderer>();
        outline = transform.Find("Outline")?.GetComponent<SpriteRenderer>();

        TagHandler handler = GetComponent<TagHandler>();
        if (!handler) handler = gameObject.AddComponent<TagHandler>();
        handler.tags.Add(Tag.Ball);

        PoolManager.instance.RegisterBall(this);
    }

    private void OnDestroy() {
        PoolManager.instance.UnregisterBall(this);
    }

    protected virtual void LateUpdate() {
        transform.right = Vector3.right;
        transform.up = Vector3.up;
    }

    protected virtual void Update() {
        if (rb.velocity.magnitude > epsilon) {
            moving = true;
            timeMoving += Time.deltaTime;
            if (timeMoving > dragThreshold)
            {
                rb.drag += dragRate;
            }
            OnMoving();
        } else if (moving) {
            timeMoving = 0;
            rb.drag = originalDrag;
            rb.velocity = Vector2.zero;
            moving = false;
            struckByBall = false;
            OnSettle();
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.HasTag(Tag.Ball) && !struckByBall) {
            if (!struckByBall) {
                struckByBall = true;
                OnHitByOtherBall();
            } else {
                OnHitOtherBall();
            }
        } else {
            OnHitNotBall();
        }
    }

    public virtual void SetOwner(int number) {
        ownerNumber = number;
    }
}
