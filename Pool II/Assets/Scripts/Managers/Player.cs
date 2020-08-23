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
        // FIXME(Simon): This method should return the number of non-cue balls tha Player has.
        return 0;
    }
}
