using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Feature
{
    public override void CloneSetup()
    {
        base.CloneSetup();
        transform.localRotation *= Quaternion.Euler(0, 0, 180);
    }
}
