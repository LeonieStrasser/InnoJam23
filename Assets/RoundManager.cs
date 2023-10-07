using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [Header("Start Balancing")]
    [HideInInspector, Min(1)] public int taskListSize = 10;
    [HideInInspector, Min(1)] public int minTaskLength = 3;
    [HideInInspector, Min(1)] public int maxTaskLength = 100;

    [Header("Progress Balancing")]
    [SerializeField, Min(0)] int additionalTasksPerRound = 3;
    [SerializeField, Min(0)] int additionalMinTaskLengthPerRound = 1;
    [SerializeField, Min(0)] int additionalMaxTaskLengthPerRound = 1;

    [Header("Testing")]
    [SerializeField, Min(0)] int playerRoundAtStart = 0;
    int roundCounter;


    public void StartFirstRound()
    {
        roundCounter = playerRoundAtStart;
        Startround(roundCounter);
    }

    public void StartNextRound()
    {
        roundCounter++;
        Startround(roundCounter);
    }

    private void Startround(int round)
    {
        TaskListManager.Instance.InitialiseList(taskListSize + additionalTasksPerRound * round,
                                                minTaskLength + additionalMinTaskLengthPerRound * round,
                                                maxTaskLength + additionalMaxTaskLengthPerRound * round);

        GameManager.Instance.StartRound();
    }
}
