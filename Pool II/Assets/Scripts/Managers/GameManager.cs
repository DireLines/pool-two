using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // load a scene for "player 1 wins"
        // "player 2 wins"
        // or "tie" based on the player_num
        switch (player_num)
        {
            case 0:
                SceneManager.LoadScene("WinPlayer1");
                break;
            case 1:
                SceneManager.LoadScene("WinPlayer2");
                break;
            case 2:
                SceneManager.LoadScene("WinTie");
                break;
        }
    }

    void OnEndTurn(TurnResult result)
    {
        // give a player another build phase turn if their balls are all gone
        int winner_num = ScoreManager.instance.CheckScore();

        Player player_1 = TurnManager.instance.players[0];
        Player player_2 = TurnManager.instance.players[1];

        int player_1_balls = player_1.BallCount();
        int player_2_balls = player_2.BallCount();

        if (winner_num == 0 || winner_num == 1)
        {
            WinGame(winner_num);
        }
        else if (player_1_balls <= 0 && player_2_balls > 0)
        {
            WinGame(1);
        }
        else if (player_2_balls <= 0 && player_1_balls > 0)
        {
            WinGame(0);
        }
        else if (player_1_balls <= 0 && player_2_balls <= 0)
        {
            WinGame(2);
        }
        else
        {

            int next_player_num = TurnManager.instance.currentPlayerIndex;
            Player next_player = TurnManager.instance.players[next_player_num];

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

    }

    private void Update()
    {
        //print($"Round: {currentRound} - BoardActive: {PoolManager.instance.boardActive}");    
    }
}
