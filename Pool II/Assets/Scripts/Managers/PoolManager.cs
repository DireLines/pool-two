using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public List<BaseBall> activeBalls;
    public bool boardActive { get; private set; }

    private const float secondsNeutralUntilRoundEnd = 1f;
    private float currentSecondsNeutral;
    private const float epsilon = 0.1f;

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
            print("Time to deactivate board!");
            boardActive = false;
            OnBoardDeactivate?.Invoke();
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
            if (ball.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > epsilon * epsilon) return false;
        }
        return true;
    }
}
