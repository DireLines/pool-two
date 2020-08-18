using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : Affecter
{
    Dictionary<Transform, Vector4> startingPos = new Dictionary<Transform, Vector4>();

    public float speed = 10f, amount = 0.15f;
    float radius = 0;
    bool shaking;

    private void Awake()
    {
        radius = amount / 2f;
        List<Transform> targets = GetTargets<Transform>();
        foreach (var target in targets)
        {
            print(target);
            startingPos[target] = new Vector4(
                target.localPosition.x-radius,
                target.localPosition.y-radius,
                Random.Range(0, 1f),
                Random.Range(0, 1f));
        }
    }

    public void Activate(float time=float.NaN)
    {
        if (shaking) return;
        StartCoroutine(ActivateCo(time));
    }

    IEnumerator ActivateCo(float time=float.NaN)
    {
        shaking = true;
        while (float.IsNaN(time) || time > 0)
        {
            foreach (var target in startingPos.Keys)
            {
                target.localPosition = (Vector2)startingPos[target] + new Vector2(
                    Mathf.PerlinNoise(Time.time * speed, startingPos[target].z) * amount, 
                    Mathf.PerlinNoise(Time.time * speed, startingPos[target].w) * amount);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        ResetPositions();
        shaking = false;
    }

    void ResetPositions() 
    {
        foreach (var target in startingPos.Keys)
        {
            target.localPosition = (Vector2)startingPos[target] + new Vector2(radius, radius);
        }
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        ResetPositions();
        shaking = false;
    }

}
