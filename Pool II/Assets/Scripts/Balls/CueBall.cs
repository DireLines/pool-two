using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : BaseBall {

    [SerializeField]
    private LineRenderer line;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask ballLayer;

    public float max_distance = 4f, power = 4f;

    private bool primed;
    private bool ready;
    private bool mouseOver;

    Vector2 direction = Vector2.zero;
    RaycastHit2D hit;


    // Start is called before the first frame update
    protected override void Start() {
        base.Start();

        if (cam == null) cam = Camera.main;

        if (line == null) line = GetComponentInChildren<LineRenderer>();
        line.useWorldSpace = true;
        primed = false;
        ready = false;
        mouseOver = false;
    }


    private void OnMouseDown() {
        if (ownerNumber != TurnManager.instance.currentPlayer) return;
        
        primed = true;
    }

    private void OnMouseUp() {
        if (ownerNumber != TurnManager.instance.currentPlayer) return;

        if (ready) {
            rb.velocity = -direction * power;
        }
        primed = false;
        ready = false;

        direction = Vector2.zero;
        line.SetPosition(0, transform.position);
        line.enabled = false;
    }

    private void OnMouseDrag() {
        if (ownerNumber != TurnManager.instance.currentPlayer) return;

        direction = ((Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position);
        if (direction.magnitude > max_distance) direction = direction.normalized * max_distance;
        line.enabled = true;
        line.SetPosition(0, (Vector2)transform.position + direction);
        line.SetPosition(1, transform.position);
    }

    private void OnMouseEnter() {
        if (ownerNumber != TurnManager.instance.currentPlayer) return;

        if (primed) ready = false;
        mouseOver = true;
    }

    private void OnMouseExit() {
        if (ownerNumber != TurnManager.instance.currentPlayer) return;

        if (primed) ready = true;
        mouseOver = false;
    }
}
