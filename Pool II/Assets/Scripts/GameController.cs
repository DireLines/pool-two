﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    GameObject game, copy;
    // Start is called before the first frame update
    void Start()
    {
        game = transform.FindDeepChild("Game").gameObject;
        copy = Instantiate(game, transform);
        copy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(game);
            game = Instantiate(copy, transform);
            game.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Destroy(copy);
            copy = Instantiate(game, transform);
            copy.SetActive(false);
        }
    }
}
