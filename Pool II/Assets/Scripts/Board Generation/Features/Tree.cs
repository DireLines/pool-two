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

    float shakeTimer, shakeLimit = 5f;

    public override void PostSetup()
    {
        base.PostSetup();
        shaker = (Shaker)gameObject.GetAffecter("MainAffecter");
        blower = GetComponent<Blower>();
        GetComponent<TagHandler>().tags.Add(Tag.Greenery);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BaseBall>())
        {
            shakeTimer = 0f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BaseBall>())
        {
            shakeTimer += Time.deltaTime;
            if (shakeTimer < shakeLimit)
            {
                if (!shaker || shaker.shaking) return;
                FX_Spawner.instance.SpawnFX(FXType.TreeRustle, transform.position, Quaternion.identity);
                shaker.Activate(0.2f);
                EmitLeaves(shakeLeafCount);
            }
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
