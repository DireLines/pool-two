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


    private void OnMouseDown() {
        if (!GameManager.instance.debug)
        {
            if (TurnManager.instance.currentPlayer == 0 &&
            !(TableZoneManager.instance.player1Zone.cueBalls.Contains(this) ||
             TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return;
            if (TurnManager.instance.currentPlayer == 1 &&
                !(TableZoneManager.instance.player2Zone.cueBalls.Contains(this) ||
                 TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return;
            if (PoolManager.instance.boardActive) return;
        }

        primed = true;
    }

    private void OnMouseUp() {
        if (!GameManager.instance.debug)
        {
            if (TurnManager.instance.currentPlayer == 0 &&
            !(TableZoneManager.instance.player1Zone.cueBalls.Contains(this) ||
             TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return;
            if (TurnManager.instance.currentPlayer == 1 &&
                !(TableZoneManager.instance.player2Zone.cueBalls.Contains(this) ||
                 TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return;
            if (PoolManager.instance.boardActive) return;
        }

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
        if (!GameManager.instance.debug)
        {
            if (TurnManager.instance.currentPlayer == 0 &&
            !(TableZoneManager.instance.player1Zone.cueBalls.Contains(this) ||
             TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return;
            if (TurnManager.instance.currentPlayer == 1 &&
                !(TableZoneManager.instance.player2Zone.cueBalls.Contains(this) ||
                 TableZoneManager.instance.neutralZone.cueBalls.Contains(this))) return;
            if (PoolManager.instance.boardActive) return;
        }

        direction = ((Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position);
        if (direction.magnitude > max_distance) direction = direction.normalized * max_distance;
        line.enabled = true;
        line.SetPosition(0, (Vector2)transform.position + direction);
        line.SetPosition(1, transform.position);
    }

    private void OnMouseEnter() {
        if (primed) ready = false;
    }

    private void OnMouseExit() {
        if (primed) ready = true;
    }
}
