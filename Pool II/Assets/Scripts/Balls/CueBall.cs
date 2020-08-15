using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CueBall : BaseBall
{
    Rigidbody2D rb;
    LineRenderer line;
    Camera cam;
    public float max_distance = 4f, power = 4f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponentInChildren<Rigidbody2D>();
        line = GetComponentInChildren<LineRenderer>();
        line.useWorldSpace = true;
        cam = Camera.main;
    }

    Vector2 direction = Vector2.zero;

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(1, body.position);
        if (Input.GetMouseButtonUp(0))
        {
            rb.velocity = -direction * power;
        }
        if (Input.GetMouseButton(0))
        {
            direction = ((Vector2)cam.ScreenToWorldPoint(Input.mousePosition) - (Vector2)body.position);
            if (direction.magnitude > max_distance) direction = direction.normalized * max_distance;
            line.enabled = true;
            line.SetPosition(0, (Vector2)body.position + direction);
        }
        else
        {
            direction = Vector2.zero;
            line.SetPosition(0, body.position);
            line.enabled = false;
        }
    }

    private void OnMouseOver()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    private void OnMouseUp()
    {

    }

    private void OnMouseDrag()
    {
        
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }
}
