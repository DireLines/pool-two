using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnResult { Default, Scratch, Build };
public delegate void TurnEvent(TurnResult result);
public delegate void GameEvent();

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public int currentRound;
    int buildThreshold = 3;


    public bool debug;

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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TurnManager.instance.NextTurn(TurnResult.Default);
        }
    }
}
