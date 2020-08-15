using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    List<Player> players = new List<Player>();
    int currentPlayer;

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

    public void NextTurn(TurnResult result)
    {
        players[currentPlayer].StartTurn(result);
        currentPlayer = ++currentPlayer;
    }

    public void EndTurn(TurnResult result)
    {
        if (currentPlayer >= players.Count)
        {
            GameManager.instance.currentRound++;
            currentPlayer = currentPlayer % players.Count;
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
