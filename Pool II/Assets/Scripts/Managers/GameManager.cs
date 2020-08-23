using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameEvent();

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public int currentRound;
    int buildThreshold = 3;


    public bool debug = true;

    public static GameManager instance;
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
        if (debug) return;
        BoardGenerator.instance.Generate();
        BoardGenerator.instance.GenerationDoneEvent += StartGame;
    }

    public void StartGame()
    {
        currentRound = 0;
        TurnManager.instance.EndTurnEvent += OnEndTurn;
        TurnManager.instance.NextTurn(TurnResult.Build);
    }

    void OnEndTurn(TurnResult result)
    {
        // evaluate win conditions
        // check score
        // handle turn conditions
        if (currentRound < buildThreshold)
        {
            TurnManager.instance.NextTurn(TurnResult.Build);
            return;
        }
        // handle result
        if (result == TurnResult.Default)
        {
            TurnManager.instance.NextTurn(TurnResult.Default);
            return;
        }
    }

    private void Update()
    {
        //print($"Round: {currentRound} - BoardActive: {PoolManager.instance.boardActive}");    
    }
}
