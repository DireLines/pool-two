using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Wind : MonoBehaviour
{
    float speed = 5f, timer = 5f;
    Vector2 intervalRange = new Vector2(5f, 15f);
    public List<Transform> targets = new List<Transform>();
    Dictionary<Transform, Feature> featureMap = new Dictionary<Transform, Feature>();
    Dictionary<Transform, Shaker> shakerMap = new Dictionary<Transform, Shaker>();

    [HideInInspector]
    bool blowing;

    public int blowLeafCount = 2;
    public ParticleSystem.MinMaxCurve blowSpeedX, blowSpeedY;

    // Start is called before the first frame update
    void Start()
    {
        BoardGenerator.instance.GenerationDoneEvent += FillBucket;
    }

    void FillBucket()
    {
        Tree[] trees = FindObjectsOfType<Tree>();
        Grass[] grass = FindObjectsOfType<Grass>();
        foreach (var feature in trees)
        {
            if (!feature.gameObject.HasTag(Tag.Greenery)) continue;
            if (featureMap.ContainsValue(feature)) continue;
            foreach (var t in (feature).shaker.GetTargets<Transform>())
            {
                featureMap[t] = feature;
                AddFeature(t);
            }
        }
        foreach (var feature in grass)
        {
            if (!feature.gameObject.HasTag(Tag.Greenery)) continue;
            if (featureMap.ContainsValue(feature)) continue;
            featureMap[feature.transform] = feature;
            AddFeature(feature.transform);
        }
        targets.Sort((p1, p2) => p1.position.x.CompareTo(p2.position.x));
        foreach (Shaker shaker in GetComponents<Shaker>())
            shaker.SetPositions();
    }

    void AddFeature(Transform t)
    {
        shakerMap[t] = gameObject.AddComponent<Shaker>();
        shakerMap[t].affecterSetting = AffecterSetting.Selected;
        shakerMap[t].selected_targets = new List<Transform>() { t };
        shakerMap[t].speed = 5f;
        shakerMap[t].amount = 0.05f;
        targets.Add(t);
    }

    // Update is called once per frame
    void Update()
    {
        if (blowing) return;
        if (timer <= 0)
        {
            FillBucket();
            // blow
            StartCoroutine(Blow());
            timer = Random.Range(intervalRange[0], intervalRange[1]);
        }
        timer -= Time.deltaTime;
    }

    IEnumerator Blow()
    {
        blowing = true;
        targets.Sort((p1, p2) => p1.position.x.CompareTo(p2.position.x));
        for (int i = targets.Count-1; i > 0; i--)
        {
            var target = targets[i];
            if (!target.gameObject.activeInHierarchy) continue;
            if (target == null) { targets.RemoveAt(i); continue; }
            if (featureMap[target] is Tree)
            {
                ((Tree)featureMap[target]).EmitLeaves(blowLeafCount, speed, blowSpeedX, blowSpeedY);
            }
            shakerMap[target].Activate(speed);
            yield return (i > 0) ? new WaitForSeconds((Mathf.Abs(targets[i - 1].position.x - target.position.x) / speed)) 
                : new WaitForSeconds(speed);
        }
        blowing = false;
    }
}
