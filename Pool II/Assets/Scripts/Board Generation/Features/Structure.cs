using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : Feature
{
    List<Collider2D> colliders = new List<Collider2D>();

    public float shakeThreshold = 8f, destroyThreshold = 12f;

    public GameObject smoothDebris;

    [HideInInspector]
    public Blower blower;

    public override void PostSetup()
    {
        base.PostSetup();
        blower = GetComponent<Blower>();
        gameObject.GetComponent<TagHandler>().tags.Add(Tag.Structure);
        colliders = new List<Collider2D>();
        foreach (var c in GetComponentsInChildren<Collider2D>())
        {
            if (c.gameObject.name != "SimulatorBody")
            {
                colliders.Add(c);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.HasTag(Tag.Ball))
        {
            float impulse = collision.contacts[0].normalImpulse;
            if (impulse > destroyThreshold)
                destroyer.Destroy();
            else if (impulse > shakeThreshold)
                destroyer.shaker.Activate(1f);
        }
    }

    protected override void Destroy()
    {
        base.Destroy();
        if (smoothDebris)
        {
            var feature = Instantiate(smoothDebris, transform.position, Quaternion.identity).GetComponent<Feature>();
            feature.transform.position = transform.position;
            feature.transform.parent = transform.parent;
            feature.PostSetup();
        }
        foreach (var c in colliders)
            c.enabled = false;
    }
}
