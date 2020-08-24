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
        ScoreManager.instance.Setup();
    }

    public void WinGame(int player_num)
    {

    }

    void OnEndTurn(TurnResult result)
    {
        // give a player another build phase turn if their balls are all gone
        int winner_num = ScoreManager.instance.CheckScore();

        if (winner_num == 0 || winner_num == 1)
        {
            WinGame(winner_num);
        }
        else
        {

            int next_player_num = TurnManager.instance.currentPlayerIndex;
            Player next_player = TurnManager.instance.players[next_player_num];

            // handle turn conditions
            if (currentRound < buildThreshold || next_player.BallCount() <= 0)
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

    }

    private void Update()
    {
        //print($"Round: {currentRound} - BoardActive: {PoolManager.instance.boardActive}");    
    }
}
