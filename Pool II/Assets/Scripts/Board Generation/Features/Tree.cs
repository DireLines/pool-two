using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Feature
{
    Shaker shaker;

    protected override void Start()
    {
        base.Start();
        shaker = (Shaker)gameObject.GetAffecter("MainAffecter");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BaseBall>())
        {
            shaker.Activate(0.2f);
        }
    }
}
