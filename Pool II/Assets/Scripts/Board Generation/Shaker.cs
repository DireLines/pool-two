using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : Affecter
{
    Dictionary<Transform, Vector3> startingPos = new Dictionary<Transform, Vector3>();

    float distance = 0.1f;

    private void Awake()
    {
        List<Transform> targets = GetTargets<Transform>();
        foreach (var target in targets)
            startingPos[target] = target.localPosition;
    }

    private void Start()
    {
        Activate(0.25f);
    }

    public void Activate(float time=-1)
    {
        StartCoroutine(ActivateCo(time));
    }

    IEnumerator ActivateCo(float time=-1)
    {
        while (time > 0)
        {
            foreach (var target in startingPos.Keys)
            {
                target.localPosition = startingPos[target] + (Random.insideUnitSphere * distance);
            }
            time -= Time.deltaTime;
            yield return null;
        }
        ResetPositions();
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
    }

}
