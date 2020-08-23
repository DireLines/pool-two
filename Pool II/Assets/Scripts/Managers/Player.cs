using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]
    bool myTurn = false;
    public static int playerCount = 0;
    public int playerNum { get; private set; }

    int ball_count = 0;

    public void StartTurn(TurnResult result)
    {
        myTurn = true;
    }

    public void EndTurn()
    {
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

    public int BallCount()
    {
        return ball_count;
    }

    public void BallLost()
    {
        ball_count--;
    }

    public void BallGained()
    {
        ball_count++;
    }
}
