using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : Affecter
{
    Dictionary<Transform, Vector3> startingPos = new Dictionary<Transform, Vector3>();

    float distance = 0.05f;
    bool shaking;

    private void Awake()
    {
        List<Transform> targets = GetTargets<Transform>();
        foreach (var target in targets)
            startingPos[target] = target.localPosition;
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
                target.localPosition = startingPos[target] + (Random.insideUnitSphere * distance);
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
            target.localPosition = startingPos[target];
        }
    }

    public void Deactivate()
    {
        StopAllCoroutines();
        ResetPositions();
        shaking = false;
    }

}
