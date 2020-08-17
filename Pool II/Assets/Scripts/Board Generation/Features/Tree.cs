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
}
