using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]
    bool myTurn = false;
    public static int playerCount = 0;
    public int playerNum { get; private set; }

    public void StartTurn(TurnResult result)
    {
        print($"It's a {result} turn!");
        myTurn = true;
    }

    public void EndTurn()
    {
        print("Ending turn!");
        myTurn = false;
    }

    void Start()
    {
        playerNum = playerCount++;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
