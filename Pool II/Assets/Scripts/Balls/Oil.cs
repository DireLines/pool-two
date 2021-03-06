﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{

    private void Start()
    {
        FX_Spawner.instance.SpawnFX(FXType.OilSplooge, transform.position, Quaternion.identity, onesie:true);
    }
    Dictionary<Rigidbody2D, float> targetBodies = new Dictionary<Rigidbody2D, float>();
    float dragReductionMultiplier = 0.25f, speedMultiplier = 0.1f, velocity_threshold = 15f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D r = collision.gameObject.GetComponent<Rigidbody2D>();
        if (collision.gameObject.HasTag(Tag.Ball) &&
            !targetBodies.ContainsKey(r))
        {
            targetBodies[r] = r.drag;
            r.drag *= dragReductionMultiplier;
            if (r.velocity.magnitude > 0 && r.velocity.magnitude < velocity_threshold)
                r.velocity *= speedMultiplier * Mathf.Log10(r.velocity.magnitude) + 1;
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
