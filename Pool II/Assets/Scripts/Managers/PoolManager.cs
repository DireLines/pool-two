using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public List<BaseBall> activeBalls;
    public bool boardActive { get; private set; }
    public bool scratchedThisTurn;
    public bool scoredThisTurn;

    private const float secondsNeutralUntilRoundEnd = 1f;
    private float currentSecondsNeutral;

    #region EVENTS
    public delegate void BoardDeactivateEvent();

    public event BoardDeactivateEvent OnBoardDeactivate;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        boardActive = false;
        currentSecondsNeutral = 0f;
        scratchedThisTurn = false;
        scoredThisTurn = false;
    }

    public void ActivateBoard()
    {
        boardActive = true;
        currentSecondsNeutral = 0f;
    }

    private void Update()
    {
        if (!boardActive) return;

        if (IsBoardNeutral())
        {
            currentSecondsNeutral += Time.deltaTime;
        }
        else
        {
            currentSecondsNeutral = 0f;
        }

        if (currentSecondsNeutral >= secondsNeutralUntilRoundEnd)
        {
            boardActive = false;
            currentSecondsNeutral = 0f;

            if (scratchedThisTurn && scoredThisTurn)
            {
                TurnManager.instance.EndTurn(TurnResult.ScoreAndScratch);
            }
            else if (scratchedThisTurn)
            {
                TurnManager.instance.EndTurn(TurnResult.Scratch);
            }
            else if (scoredThisTurn)
            {
                TurnManager.instance.EndTurn(TurnResult.Score);
            }
            else
            {
                TurnManager.instance.EndTurn(TurnResult.Default);
            }

            scratchedThisTurn = false;
            scoredThisTurn = false;
        }
    }

    public void RegisterBall(BaseBall ball)
    {
        if (activeBalls == null) activeBalls = new List<BaseBall>();
        activeBalls.Add(ball);
    }

    public void UnregisterBall(BaseBall ball)
    {
        activeBalls.Remove(ball);
    }

    public bool IsBoardNeutral()
    {
        foreach (var ball in activeBalls)
        {
            if (ball.moving) return false;
        }
        return true;
    }
}
