using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnResult { Default, Scratch, Build, Score, ScoreAndScratch, };
public delegate void TurnEvent(TurnResult result);

public class TurnManager : MonoBehaviour {
    public static TurnManager instance;

    public TurnResult lastTurnResult = TurnResult.Default;

    public List<Player> players = new List<Player>();
    public int currentPlayer;// { get; private set; }

    public TurnEvent EndTurnEvent;

    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        players = new List<Player>(FindObjectsOfType<Player>());
        currentPlayer = Random.Range(0, players.Count);
        //ScoreUIManager.instance.SetActiveCursor(currentPlayer);
    }

    private void Start() {
        ScoreUIManager.instance.SetActiveCursor(currentPlayer);
        PoolManager.instance.OnBoardDeactivate += EndTurnDefault;
    }

    public void EndTurnDefault() {
        print("NEXT TURN!");
        EndTurn(TurnResult.Default);
    }

    public void EndTurnScratch() {
        EndTurn(TurnResult.Scratch);
    }

    public void EndTurnBuild() {
        EndTurn(TurnResult.Build);
    }

    public void NextTurn(TurnResult result) {
        if (players.Count == 0) return;
        lastTurnResult = result;
        players[currentPlayer].StartTurn(result);
    }

    public void EndTurn(TurnResult result) {
        currentPlayer++;
        if (currentPlayer >= players.Count) {
            GameManager.instance.currentRound++;
            currentPlayer = 0;
        }

        print($"Player {currentPlayer + 1}'s turn!");
        ScoreUIManager.instance.SetActiveCursor(currentPlayer);

        //if (currentPlayer == 0 && TableZoneManager.instance.player1Zone.cueBalls.Count == 0 && TableZoneManager.instance.neutralZone.cueBalls.Count == 0)
        //{
        //    currentPlayer = 1;
        //}
        //else if (currentPlayer == 1 && TableZoneManager.instance.player2Zone.cueBalls.Count == 0 && TableZoneManager.instance.neutralZone.cueBalls.Count == 0)
        //{
        //    currentPlayer = 0;
        //}

        players[currentPlayer].EndTurn();
        EndTurnEvent?.Invoke(result);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            EndTurn(TurnResult.Default);
        }
    }
}
