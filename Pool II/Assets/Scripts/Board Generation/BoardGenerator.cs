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
    [HideInInspector]
    public Transform player1Board;
    [HideInInspector]
    public Transform player2Board;
    public Board playerBoard, neutralBoard;
    bool simulating = false;
    public GameEvent GenerationDoneEvent;

    public static BoardGenerator instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        player1Board = transform.FindDeepChild("Player1Board");
    }

    public void Generate()
    {
        StartCoroutine(CreateBoard());
    }

    IEnumerator CreateBoard()
    {
        Time.timeScale = 3f;
        neutralBoard.gameObject.SetActive(false);
        Aggregate(playerFeatureSets, playerBoard);
        while (simulating) yield return null;
        neutralBoard.gameObject.SetActive(true);
        playerBoard.DeactivateWalls();
        playerBoard.gameObject.SetActive(false);
        Aggregate(neutralFeatureSets, neutralBoard);
        while (simulating) yield return null;
        neutralBoard.DeactivateWalls();
        playerBoard.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        player2Board = Instantiate(player1Board.gameObject, transform).transform;
        player2Board.gameObject.name = "Player2Board";
        player2Board.localScale = new Vector3(-player2Board.localScale.x, -player2Board.localScale.y, player2Board.localScale.z);
        foreach (var feature in player2Board.GetComponentsInChildren<Feature>())
        {
            feature.CloneSetup();
            feature.transform.localScale = new Vector3(
                feature.transform.localScale.x, 
                -feature.transform.localScale.y, 
                feature.transform.localScale.z);
        }
        foreach (var feature in FindObjectsOfType<Feature>()) feature.PostSetup();
        GenerationDoneEvent?.Invoke();
        Time.timeScale = 1f;
    }

    void Aggregate(List<FeatureSet> featureSets, Board board) 
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
        yield return new WaitForSeconds(2f);
        foreach (var feature in features)
        {
            feature.Place();
        }
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
                script.playerBoard.SetupWalls();
                script.neutralBoard.SetupWalls();
            }
    }
}
#endif