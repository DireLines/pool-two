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
}