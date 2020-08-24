using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D), typeof(TagHandler))]
public class BaseBall : MonoBehaviour {
    public int cost;
    [HideInInspector]
    public int ownerNumber = -1;// {get; private set; }

    protected SpriteRenderer icon;
    protected SpriteRenderer inside;
    protected SpriteRenderer outline;

    protected Rigidbody2D rb;

    public bool moving { get; private set; }

    public bool struckByBall { get; private set; }

    float epsilon = 0.1f;

    float timeMoving, dragThreshold = 5f, dragRate = 1f, originalDrag;

    float collisionFXThreshold = 10f, collisionFXMin = 4f;

    public Color player1Color;
    public Color player2Color;


    protected Animator anim;

    protected virtual void OnHitByOtherBall(GameObject other, Collision2D collision) { }
    protected virtual void OnHitOtherBall(GameObject other, Collision2D collision) { }
    protected virtual void OnHitNotBall(GameObject other, Collision2D collision) { }
    protected virtual void OnMoving() { }
    protected virtual void OnSettle() { }
    public virtual void OnSink() {
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

        inside.color = ownerNumber == 0 ? player1Color : player2Color;
    }

    private void OnDestroy() {
        if (ownerNumber > 0 && ownerNumber < 2) {
            PoolManager.instance.UnregisterBall(this);
            Player owner = TurnManager.instance.players[ownerNumber];
            owner.BallLost();
        }
    }

    protected virtual void LateUpdate() {
        transform.right = Vector3.right;
        transform.up = Vector3.up;
    }

    protected virtual void Update() {
        if (rb.velocity.magnitude > epsilon) {
            moving = true;
            timeMoving += Time.deltaTime;
            if (timeMoving > dragThreshold) {
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
        CollisionSound(other, collision);
        // TODO(Simon): This logic might be wrong, consider the blocking tag
        if (other.HasTag(Tag.Ball) && !other.HasTag(Tag.Blocking)) {
            if (!struckByBall) {
                struckByBall = true;
                OnHitByOtherBall(other, collision);
            } else {
                OnHitOtherBall(other, collision);
            }
        } else {
            OnHitNotBall(other, collision);
        }
    }

    void CollisionSound(GameObject other, Collision2D collision) {
        FXType effectName = FXType.Default;
        if (other.HasTag(Tag.Ball))
            effectName = FXType.BallToBall;
        else
            effectName = FXType.BallToWall;
        var vol = Mathf.Clamp(collision.contacts[0].normalImpulse, 0, collisionFXThreshold) / collisionFXThreshold;
        FX_Spawner.instance.SpawnFX(effectName, transform.position, Quaternion.identity, vol: vol);
    }

    public virtual void SetOwner(int number) {
        ownerNumber = number;
    }
}
