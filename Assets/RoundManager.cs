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

    [Header("Testing")]
    [SerializeField, Min(0)] int playerRoundAtStart = 0;
    int roundCounter;

    private Clock TimerClock;

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

        TimerClock.StartTimer(secondsUntilDeathline+additionalSecondsUntilDeathline * round);

        GameManager.Instance.StartRound();
    }
}
