using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Feature
{
    Shaker shaker;

    protected override void Start()
    {
        base.Start();
        shaker = GetComponent<Shaker>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        print("bite me retard");
        if (collision.GetComponent<BaseBall>())
        {
            print("Mega bite me retard");
            shaker.Activate(0.2f);
        }
    }
}
