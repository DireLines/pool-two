using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Player> players = new List<Player>();
    public int currentPlayer;// { get; private set; }

    public TurnEvent EndTurnEvent;

    public static TurnManager instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        players = new List<Player>(FindObjectsOfType<Player>());
        currentPlayer = Random.Range(0, players.Count);
    }

    private void Start()
    {
        PoolManager.instance.OnBoardDeactivate += EndTurnDefault;
    }

    public void EndTurnDefault()
    {
        print("NEXT TURN!");
        EndTurn(TurnResult.Default);
    }

    public void EndTurnScratch()
    {
        EndTurn(TurnResult.Scratch);
    }

    public void EndTurnBuild()
    {
        EndTurn(TurnResult.Build);
    }

    public void NextTurn(TurnResult result)
    {
        if (players.Count == 0) return;
        players[currentPlayer].StartTurn(result);
    }

    public void EndTurn(TurnResult result)
    {
        currentPlayer++;
        if (currentPlayer >= players.Count)
        {
            GameManager.instance.currentRound++;
            currentPlayer = 0;
        }
        players[currentPlayer].EndTurn();
        EndTurnEvent?.Invoke(result);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            EndTurn(TurnResult.Default);
        }
    }
}
