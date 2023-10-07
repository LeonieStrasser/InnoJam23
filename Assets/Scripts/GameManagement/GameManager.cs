using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum GameState
    {
        RoundInProgress, RoundFinished, GameOver
    }

    [SerializeField] private float DeadlineTime;

    private GameState CurrentGameState;

    private DreamBubble Bubble;
    private Clock TimerClock;

    [SerializeField] RoundManager roundManager;

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Bubble = FindObjectOfType<DreamBubble>();
        Bubble.OnMaxTiredness += TirednessGameOver;

        TimerClock = FindObjectOfType<Clock>();
        TimerClock.OnTimerFinished += DeadlineReached;

        roundManager.StartFirstRound();
    }

    public void StartRound()
    {
        Bubble.Reset();
        TimerClock.StartTimer(DeadlineTime);

        // fill list
        // start clock

        CurrentGameState = GameState.RoundInProgress;
    }

    private void TirednessGameOver()
    {
        Debug.LogWarning("GAME OVER: Too tired.");
        CurrentGameState = GameState.GameOver;
    }

    private void DeadlineGameOver()
    {
        Debug.LogWarning("GAME OVER: Not productive enough.");
        CurrentGameState = GameState.GameOver;
    }

    public void SuccessfullDeathline()
    {
        Debug.Log("Deadline reached, finished enough tasks, SUCCESS");
        CurrentGameState = GameState.RoundFinished;

        // do magic

        roundManager.StartNextRound();
    }

    private void DeadlineReached()
    {
        // check work amount
        if (TaskListManager.Instance.AreRequiredWorkTaskFinished)
            SuccessfullDeathline();
        else
            DeadlineGameOver();

        //DeadlineGameOver();
    }

    private void OnDestroy()
    {
        Bubble.OnMaxTiredness -= TirednessGameOver;
        TimerClock.OnTimerFinished -= DeadlineReached;
    }
}
