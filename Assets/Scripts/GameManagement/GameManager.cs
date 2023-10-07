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

    private GameState CurrentGameState;
    
    private DreamBubble Bubble;
    private Clock TimerClock;
    
    private void Start()
    {
        Bubble = FindObjectOfType<DreamBubble>();
        Bubble.OnMaxTiredness += TirednessGameOver;
        
        TimerClock = FindObjectOfType<Clock>();
        TimerClock.OnTimerFinished += DeadlineReached;
        
        StartRound();
    }

    private void StartRound()
    {
        Bubble.Reset();
        TimerClock.StartTimer(120f);
        
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
        Debug.LogWarning("GAME OVER: Too tired.");
        CurrentGameState = GameState.GameOver;
    }

    private void DeadlineReached()
    {
        // check work amount
        Debug.Log("Deadline reached, checking finished workload...");
        
        //DeadlineGameOver();
    }

    private void OnDestroy()
    {
        Bubble.OnMaxTiredness -= TirednessGameOver;
        TimerClock.OnTimerFinished -= DeadlineReached;
    }
}