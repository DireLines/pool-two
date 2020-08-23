using System.Collections;
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

    protected Animator anim;

    protected virtual void OnHitByOtherBall(GameObject other) { }
    protected virtual void OnHitOtherBall(GameObject other) { }
    protected virtual void OnHitNotBall(GameObject other) { }
    protected virtual void OnMoving() { }
    protected virtual void OnSettle() { }
    public virtual void OnSink() 
    { 
        PoolManager.instance.UnregisterBall(this);
    }

    protected virtual void Start() {
        anim = GetComponent<Animator>();
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

        Player owner = TurnManager.instance.players[ownerNumber];

        owner.BallLost();
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
        GameObject other = collision.gameObject;
        // TODO(Simon): This logic might be wrong, consider the blocking tag
        if (other.HasTag(Tag.Ball) && !other.HasTag(Tag.Blocking)) {
            if (!struckByBall) {
                struckByBall = true;
                OnHitByOtherBall(other);
            } else {
                OnHitOtherBall(other);
            }
        } else {
            OnHitNotBall(other);
        }
    }

    public virtual void SetOwner(int number) {
        ownerNumber = number;
    }
}
