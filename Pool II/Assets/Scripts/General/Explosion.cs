using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float duration = 0.2f, magnitude = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        CameraShake.instance.TriggerShake(duration, magnitude);
    }
}
