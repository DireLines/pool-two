using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Feature
{
    public override void PostSetup()
    {
        base.PostSetup();
        GetComponent<TagHandler>().tags.Add(Tag.Greenery);
    }
}
