using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // player number mapped to score
    int[] scoreMap = { 0, 0 };
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

    private void Start()
    {
        UpdateScore(1, 0);
        UpdateScore(2, 0);
    }

    public void Setup()
    {
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
        scoreMap[playerNumber-1] += score;
    }

    void Scratch(int playerNumber)
    {
        OnSadEvent?.Invoke(playerNumber);
    }
}
