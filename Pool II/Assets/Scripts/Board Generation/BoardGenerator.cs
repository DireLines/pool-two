using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{

    public List<FeatureSet> featureSets = new List<FeatureSet>();
    List<Feature> features = new List<Feature>();

    // Start is called before the first frame update
    void Start()
    {
        Generate();
    }

    public void Generate() 
    {
        foreach (var featureSet in featureSets)
        {
            features.AddRange(featureSet.Populate());
        }

        StartCoroutine(Simulate());
    }

    IEnumerator Simulate()
    {
        yield return new WaitForSeconds(3f);
        foreach (var feature in features)
        {
            feature.Place();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
