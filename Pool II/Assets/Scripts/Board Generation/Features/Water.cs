using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Feature
{
    protected override void Start()
    {
        base.Start();
        body.GetComponent<AreaEffector2D>().forceMagnitude *= Random.Range(-1f, 1f);
    }
}
