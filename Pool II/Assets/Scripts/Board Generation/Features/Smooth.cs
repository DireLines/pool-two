using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smooth : Feature
{
    Dictionary<Rigidbody2D, float> targetBodies = new Dictionary<Rigidbody2D, float>();
    float dragReductionMultiplier = 0.25f, speedMultipler = 1.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D r = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.HasTag(Tag.Ball) && 
            !targetBodies.ContainsKey(r))
        {
            targetBodies[r] = r.drag;
            r.drag *= dragReductionMultiplier;
            r.velocity *= speedMultipler;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D r = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.HasTag(Tag.Ball) &&
            targetBodies.ContainsKey(r))
        {
            r.drag = targetBodies[r];
            targetBodies.Remove(r);
        }
    }
}
