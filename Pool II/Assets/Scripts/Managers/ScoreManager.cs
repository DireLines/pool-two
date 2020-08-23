using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // player number mapped to score
    Dictionary<int, int> scoreMap = new Dictionary<int, int>();

    public static ScoreManager instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        foreach (var pocket in GameObject.Find("Player1Board").GetComponentsInChildren<HoleController>())
        {
            pocket.ownerNumber = 1;
            pocket.OnScoreEvent += delegate { UpdateScore(1, 1); };
        }
        foreach (var pocket in GameObject.Find("Player2Board").GetComponentsInChildren<HoleController>())
        {
            pocket.ownerNumber = 2;
            pocket.OnScoreEvent += delegate { UpdateScore(2, 1); };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateScore(int playerNumber, int score)
    {

    }
}
