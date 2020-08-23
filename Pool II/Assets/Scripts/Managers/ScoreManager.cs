using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // player number mapped to score
    Dictionary<int, int> scoreMap = new Dictionary<int, int>() { {0,0}, {1,0} };
    public List<GameObject> playerBoards;
    public static event EmotiveEvent OnHappy, OnSadEvent;

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
        UpdateScore(0, 0);
        UpdateScore(1, 0);
        foreach (var pocket in GameObject.Find("Player1Board").GetComponentsInChildren<HoleController>())
        {
            pocket.ownerNumber = 1;
            pocket.OnScoreEvent += delegate { UpdateScore(1, 1); };
            pocket.OnScratchEvent += delegate { Scratch(0); };
        }
        foreach (var pocket in GameObject.Find("Player2Board").GetComponentsInChildren<HoleController>())
        {
            pocket.ownerNumber = 2;
            pocket.OnScoreEvent += delegate { UpdateScore(1, 1); };
            pocket.OnScratchEvent += delegate { Scratch(1); };
        }
    }
    void UpdateScore(int playerNumber, int score)
    {
        
        scoreMap[playerNumber] += score;
        OnHappy?.Invoke(playerNumber);
        ScoreUIManager.instance.SetScore(playerNumber, score);
    }

    void Scratch(int playerNumber)
    {
        OnSadEvent?.Invoke(playerNumber);
    }
}
