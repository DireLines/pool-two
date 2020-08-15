using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    List<Player> players = new List<Player>();
    int currentPlayer;

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

        NextTurn(true);
    }

    public void NextTurn(bool first = false)
    {
        if (!first) players[currentPlayer].EndTurn();
        players[currentPlayer].StartTurn();
        currentPlayer = ++currentPlayer % players.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            NextTurn();
        }
    }
}
