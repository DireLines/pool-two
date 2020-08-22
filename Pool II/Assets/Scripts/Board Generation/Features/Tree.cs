using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Feature
{
    [HideInInspector]
    public Shaker shaker;
    [HideInInspector]
    public Blower blower;
    int shakeLeafCount = 1;

    public override void PostSetup()
    {
        base.PostSetup();
        shaker = (Shaker)gameObject.GetAffecter("MainAffecter");
        blower = GetComponent<Blower>();
        GetComponent<TagHandler>().tags.Add(Tag.Greenery);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BaseBall>())
        {
            if (!shaker || shaker.shaking) return;
            shaker.Activate(0.5f);
            EmitLeaves(shakeLeafCount);
        }
    }

    public void EmitLeaves(int leafCount)
    {
        foreach (var leaf in blower.particles)
        {
            leaf.Emit(leafCount);
        }
    }
}
