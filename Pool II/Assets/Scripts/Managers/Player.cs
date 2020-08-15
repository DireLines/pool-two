﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [HideInInspector]
    bool myTurn = false;

    public void StartTurn(TurnResult result)
    {
        print(gameObject + ":");
        print($"It's a {result} turn!");
        myTurn = true;
    }

    public void EndTurn()
    {
        print("Ending turn!");
        myTurn = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}