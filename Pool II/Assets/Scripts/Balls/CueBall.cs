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

    List<Rect> playableAreas;

    protected override void Start() {
        base.Start();

        if (cam == null) cam = Camera.main;

        if (line == null) line = GetComponentInChildren<LineRenderer>();
        line.useWorldSpace = true;
        primed = false;
        ready = false;
    }


    private void OnMouseDown() {
        if (ownerNumber != TurnManager.instance.currentPlayer) return;
        if (PoolManager.instance.boardActive) return;

        primed = true;
    }

    private void OnMouseUp() {
        if (ownerNumber != TurnManager.instance.currentPlayer) return;

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
        if (ownerNumber != TurnManager.instance.currentPlayer) return;
        if (PoolManager.instance.boardActive) return;

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
