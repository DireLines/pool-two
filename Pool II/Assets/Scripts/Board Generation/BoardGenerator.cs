using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

public class BoardGenerator : MonoBehaviour
{

    public List<FeatureSet> playerFeatureSets = new List<FeatureSet>(), neutralFeatureSets = new List<FeatureSet>();

    Transform pivot;
    public Board playerBoard, neutralBoard;

    bool simulating = false;

    // Start is called before the first frame update
    void Start()
    {
        pivot = transform.FindDeepChild("BoardPivot");
        StartCoroutine(CreateBoard());
    }

    IEnumerator CreateBoard()
    {
        neutralBoard.gameObject.SetActive(false);
        Generate(playerFeatureSets, playerBoard);
        while (simulating) yield return null;
        neutralBoard.gameObject.SetActive(true);
        playerBoard.DeactivateWalls();
        playerBoard.gameObject.SetActive(false);
        Generate(neutralFeatureSets, neutralBoard);
        while (simulating) yield return null;
        neutralBoard.DeactivateWalls();
        playerBoard.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        Transform p = Instantiate(pivot.gameObject, transform).transform;
        p.localScale = new Vector3(-p.localScale.x, -p.localScale.y, p.localScale.z);
        foreach (var feature in p.GetComponentsInChildren<Feature>())
        {
            feature.transform.localScale = new Vector3(
                feature.transform.localScale.x, 
                -feature.transform.localScale.y, 
                feature.transform.localScale.z);
        }
    }

    public void Generate(List<FeatureSet> featureSets, Board board) 
    {
        List<Feature> features = new List<Feature>();
        foreach (var featureSet in featureSets)
        {
            features.AddRange(featureSet.Populate(board));
        }

        StartCoroutine(Simulate(features));
    }

    IEnumerator Simulate(List<Feature> features)
    {
        simulating = true;
        yield return new WaitForSeconds(1f);
        foreach (var feature in features)
        {
            feature.Place();
        }
        print(simulating);
        simulating = false;
    }
}

# if UNITY_EDITOR
[CanEditMultipleObjects]
[CustomEditor(typeof(BoardGenerator))]
public class BoardGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var scripts = targets.OfType<BoardGenerator>();
        if (GUILayout.Button("GENERATE BOUNDS"))
            foreach (var script in scripts) {
                var pivot = script.transform.FindDeepChild("BoardPivot");
                var scale = pivot.localScale;
                pivot.localScale = Vector3.one; 
                script.playerBoard.SetupWalls();
                script.neutralBoard.SetupWalls();
                pivot.localScale = scale;
            }
    }
}
#endif