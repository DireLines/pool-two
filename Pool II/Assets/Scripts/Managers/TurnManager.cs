using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurnResult { Default, Scratch, Build, Score, ScoreAndScratch, };
public delegate void TurnEvent(TurnResult result);

public class TurnManager : MonoBehaviour {
    public static TurnManager instance;

    public TurnResult lastTurnResult = TurnResult.Default;

    public List<Player> players = new List<Player>();
    public int currentPlayerIndex;// { get; private set; }

    public Player currentPlayer() {
        return players[currentPlayerIndex];
    }

    public TurnEvent EndTurnEvent;

    private void Awake() {
        if (instance) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        players = new List<Player>(FindObjectsOfType<Player>());
        currentPlayerIndex = Random.Range(0, players.Count);
        //ScoreUIManager.instance.SetActiveCursor(currentPlayer);
    }

    private void Start() {
        ScoreUIManager.instance.SetActiveCursor(currentPlayerIndex);
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
        players[currentPlayerIndex].StartTurn(result);
    }

    public void EndTurn(TurnResult result) {
        currentPlayerIndex++;
        if (currentPlayerIndex >= players.Count) {
            GameManager.instance.currentRound++;
            currentPlayerIndex = 0;
        }

        print($"Player {currentPlayerIndex + 1}'s turn!");
        ScoreUIManager.instance.SetActiveCursor(currentPlayerIndex);

        //if (currentPlayer == 0 && TableZoneManager.instance.player1Zone.cueBalls.Count == 0 && TableZoneManager.instance.neutralZone.cueBalls.Count == 0)
        //{
        //    currentPlayer = 1;
        //}
        //else if (currentPlayer == 1 && TableZoneManager.instance.player2Zone.cueBalls.Count == 0 && TableZoneManager.instance.neutralZone.cueBalls.Count == 0)
        //{
        //    currentPlayer = 0;
        //}

        players[currentPlayerIndex].EndTurn();
        EndTurnEvent?.Invoke(result);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Jump")) {
            EndTurn(TurnResult.Default);
        }
    }
}
