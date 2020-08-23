using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // player number mapped to score
    Dictionary<int, int> scoreMap = new Dictionary<int, int>() { {1,0}, {2,0} };
    public List<GameObject> playerBoards;
    public static event EmotiveEvent OnHappy, OnSadEvent;

    [SerializeField]
    int score_target;

    public static ScoreManager instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    public void Setup()
    {
        UpdateScore(1, 0);
        UpdateScore(2, 0);
        foreach (var pocket in GameObject.Find("Player1Board").GetComponentsInChildren<HoleController>())
        {
            pocket.ownerNumber = 1;
            pocket.OnScoreEvent += delegate { UpdateScore(1, 1); };
            pocket.OnScratchEvent += delegate { Scratch(1); };
        }
        foreach (var pocket in GameObject.Find("Player2Board").GetComponentsInChildren<HoleController>())
        {
            pocket.ownerNumber = 2;
            pocket.OnScoreEvent += delegate { UpdateScore(2, 1); };
            pocket.OnScratchEvent += delegate { Scratch(2); };
        }
    }
    void UpdateScore(int playerNumber, int score)
    {
        
        scoreMap[playerNumber] += score;
        OnHappy?.Invoke(playerNumber);
        ScoreUIManager.instance.SetScore(playerNumber - 1, score);
    }

    void Scratch(int playerNumber)
    {
        OnSadEvent?.Invoke(playerNumber);
    }

    public int CheckScore()
    {
        int p1_score = scoreMap[1];
        int p2_score = scoreMap[2];

        if (p1_score > p2_score && p1_score > score_target)
        {
            return 1;
        }
        else if (p2_score > p1_score && p2_score > score_target)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
