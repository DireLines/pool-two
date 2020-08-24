using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour {
    [HideInInspector]
    public int ownerNumber;

    Collider2D holeCollider;

    List<GameObject> sunkBalls = new List<GameObject>();
    float tumult = 2f, tumultThreshold = 8f;

    public GameEvent OnScoreEvent;
    public GameEvent OnScratchEvent;

    private void Start()
    {
        foreach (var collider in GetComponentsInChildren<Collider2D>())
        {
            if (collider.isTrigger)
            {
                holeCollider = collider;
                return;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (!collision.gameObject.HasTag(Tag.Ball) || sunkBalls.Contains(collision.gameObject)) return;
        Rigidbody2D r = collision.GetComponent<Rigidbody2D>();
        if (r.velocity.magnitude > tumultThreshold) r.AddForce(Random.insideUnitCircle * tumult, ForceMode2D.Impulse);
        if (holeCollider.OverlapPoint(collision.transform.position))
        {
            var ball = collision.GetComponent<BaseBall>();
            if (collision.GetComponent<CueBall>())
            {
                FX_Spawner.instance.SpawnFX(FXType.ScratchBall, transform.position, Quaternion.identity);
                PoolManager.instance.scratchedThisTurn = true;
            }
            else
            {
                // sinky the stinky
                if (ball.ownerNumber != ownerNumber)
                {
                    OnScoreEvent?.Invoke();
                    FX_Spawner.instance.SpawnFX(FXType.SinkBall, transform.position, Quaternion.identity);
                    PoolManager.instance.scoredThisTurn = true;
                }
                else
                {
                    FX_Spawner.instance.SpawnFX(FXType.ScratchBall, transform.position, Quaternion.identity);
                    PoolManager.instance.scratchedThisTurn = true;
                }
            }

            sunkBalls.Add(collision.gameObject);
            ball.OnSink();
            StartCoroutine(EnterHole(r));
        }
    }

    private IEnumerator EnterHole(Rigidbody2D rb) {
        Vector2 direction;
        float t = 0f, timer = 0.5f;
        rb.velocity = ((Vector2)transform.position - rb.position).normalized * 10f;
        float initialDrag = rb.drag;
        rb.drag = 10f;
        Vector3 scale = rb.transform.localScale, targetScale = rb.transform.localScale * 0.2f;
        SpriteRenderer r = rb.GetComponentInChildren<SpriteRenderer>();
        Color color = r.color;
        while (t < timer) {
            rb.transform.localScale = Vector3.Slerp(scale, targetScale, t/timer);
            r.color = Color.Lerp(color, Color.black, t/timer);
            direction = ((Vector2)transform.position - rb.position).normalized;
            rb.velocity = direction * rb.velocity.magnitude * (1 - 2f * Time.deltaTime);
            Debug.DrawRay(rb.position, rb.velocity, Color.cyan);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        rb.drag = initialDrag;
        rb.transform.localScale = scale;
        r.color = color;

        if (rb.gameObject.HasTag(Tag.Cue))
        {
            BuildController.instance.hold(rb.gameObject);
        }
        Destroy(rb.gameObject);
    }
}
