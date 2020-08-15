using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Feature Set", menuName = "ScriptableObjects/Feature Set", order = 1)]
public class FeatureSet : ScriptableObject
{
    public int small_count, medium_count, large_count;
    public List<GameObject> small_features;
    public List<GameObject> medium_features;
    public List<GameObject> large_features;

    public List<Feature> Populate()
    {
        List<Feature> features = new List<Feature>();
        features.AddRange(Select(small_count, small_features));
        features.AddRange(Select(medium_count, medium_features));
        features.AddRange(Select(large_count, large_features));
        return features;
    }

    List<Feature> Select(int count, List<GameObject> set)
    {
        List<Feature> features = new List<Feature>();
        for (int i=0; i < count; i ++)
        {
            features.Add(Instantiate(set[Random.Range(0, set.Count)], Board.instance.RandomPointInBounds(), Quaternion.identity).GetComponent<Feature>());
        }

        return features;
    }
}