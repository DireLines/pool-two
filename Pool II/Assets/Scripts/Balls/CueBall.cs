using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : BaseBall {

    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Camera cam;

    public float max_distance = 4f, power = 4f;

    private bool primed;
    private bool ready;

    Vector2 direction = Vector2.zero;

    protected override void Start() {
        base.Start();

        if (cam == null) cam = Camera.main;

        if (line == null) line = GetComponentInChildren<LineRenderer>();
        line.useWorldSpace = true;
        primed = false;
        ready = false;
    }

    private bool CheckClickable()
    {
        if (GameManager.instance.debug) return true;
        if (PoolManager.instance.boardActive) return false;
        if (TurnManager.instance.currentPlayer == 0)
        {
            if (TableZoneManager.instance.player1Zone.cueBalls.Count + TableZoneManager.instance.neutralZone.cueBalls.Count > 0)
            {
                if (!(TableZoneManager.instance.player1Zone.cueBalls.Contains(this) ||
                    TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return false;
            }
        }
        else if (TurnManager.instance.currentPlayer == 1)
        {
            if (TableZoneManager.instance.player2Zone.cueBalls.Count + TableZoneManager.instance.neutralZone.cueBalls.Count > 0)
            {
                if (!(TableZoneManager.instance.player2Zone.cueBalls.Contains(this) ||
                    TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return false;
            }
        }
        return true;
    }


    private void OnMouseDown() {
        if (!CheckClickable()) return;

        primed = true;
    }

    private void OnMouseUp() {
        if (!CheckClickable()) return;

        if (ready) {
            rb.velocity = -direction * power;
            PoolManager.instance.ActivateBoard();
        }
        primed = false;
        ready = false;

        direction = Vector2.zero;
        line.SetPosition(0, transform.position);
        line.enabled = false;
    }

    private void OnMouseDrag() {
        if (!CheckClickable()) return;

        direction = ((Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position);
        if (direction.magnitude > max_distance) direction = direction.normalized * max_distance;
        line.enabled = true;
        line.SetPosition(0, (Vector2)transform.position + direction);
        line.SetPosition(1, transform.position);
    }

    private void OnMouseEnter() {
        anim.SetBool("Blink", true);
        if (primed)
        {
            ready = false;
        }
    }

    private void OnMouseExit() {
        anim.SetBool("Blink", false);
        if (primed) 
        {
            ready = true; 
        }
    }

    protected override void OnHitByOtherBall(GameObject other, Collision2D collision) {
        var myEmoter = GetComponent<EmotionListener>();
        var cue = other.GetComponent<CueBall>();

        // If both are Cueballs, I get mad, they get happy
        if(cue != null) {
            var emoter = GetComponent<EmotionListener>();
            emoter.OnHappy(cue.ownerNumber);

        }

        myEmoter.OnMad();
    }
}
