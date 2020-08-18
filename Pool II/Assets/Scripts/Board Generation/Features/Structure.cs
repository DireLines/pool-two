using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Feature
{
    List<Collider2D> colliders = new List<Collider2D>();

    public float shakeThreshold = 3f, destroyThreshold = 6f;

    protected override void Start()
    {
        base.Start();
        colliders = new List<Collider2D>(GetComponentsInChildren<Collider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.HasTag(Tag.Ball))
        {
            float impulse = collision.contacts[0].normalImpulse;
            if (impulse > shakeThreshold)
                destroyer.shaker.Activate(1f);
            else if (impulse > destroyThreshold)
                destroyer.Destroy();
        }
    }

    protected override void Destroy()
    {
        base.Destroy();
        foreach (var c in colliders)
            c.enabled = false;
    }
}
