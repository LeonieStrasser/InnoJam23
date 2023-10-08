using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Start Balancing")]
    [SerializeField, Min(1)] int taskListSize = 10;
    [SerializeField, Min(1)] int minTaskLength = 3;
    [SerializeField, Min(1)] int maxTaskLength = 100;
    [Space]
    [SerializeField, Min(15)] int secondsUntilDeathline = 15;

    [Header("Progress Balancing")]
    [SerializeField, Min(0)] int additionalTasksPerRound = 3;
    [SerializeField, Min(0)] int additionalMinTaskLengthPerRound = 1;
    [SerializeField, Min(0)] int additionalMaxTaskLengthPerRound = 1;
    [Space]
    [SerializeField] int additionalSecondsUntilDeathline = 0;
    [Space]
    [SerializeField, Range(0f, 1f)] float keptWhiteSheep = 0.25f;
    [SerializeField, Range(0f, 1f)] float keptBlackSheep = 0.9f;

    [Header("Testing")]
    [SerializeField, Min(0)] int playerRoundAtStart = 0;
    int roundCounter;

    private Clock TimerClock;

    public float NormalisedPassedTime
    {
        get
        {
            if (TimerClock == null)
                TimerClock = FindObjectOfType<Clock>();

            return TimerClock.NormalisedPassedTime;
        }
    }

    private static RoundManager instance;
    public static RoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<RoundManager>();
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    public void StartFirstRound()
    {
        roundCounter = playerRoundAtStart;
        StartRound(roundCounter);
    }

    public void StartNextRound()
    {
        roundCounter++;
        StartRound(roundCounter);
    }

    private void StartRound(int round)
    {
        if (TimerClock == null)
            TimerClock = FindObjectOfType<Clock>();

        TaskListManager.Instance.InitialiseList(taskListSize + additionalTasksPerRound * round,
                                                minTaskLength + additionalMinTaskLengthPerRound * round,
                                                maxTaskLength + additionalMaxTaskLengthPerRound * round);

        TimerClock.StartTimer(secondsUntilDeathline + additionalSecondsUntilDeathline * round);

        GameManager.Instance.Bubble.RemoveSheep(keptWhiteSheep, keptBlackSheep);
        CoffeMug.Instance.ResetCoffee();
        GameManager.Instance.StartRound();
    }

    //[ContextMenu("Continue")]
    //public void Continue()
    //{
    //    TimerClock.Continue();
    //    TaskInputReceiver.Instance.SetInputBlocked(false);

    //    Time.timeScale = 1;
    //}

    //[ContextMenu("Pause")]
    //public void Pause()
    //{
    //    TimerClock.Pause();
    //    TaskInputReceiver.Instance.SetInputBlocked(true);

    //    Time.timeScale = 0;
    //}
}
