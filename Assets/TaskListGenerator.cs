using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListGenerator : MonoBehaviour
{
    [SerializeField] TextAsset workTaskData;

    public List<string> GenerateList(int listLength, int minTaskLength = 0, int maxTaskLength = int.MaxValue)
    {
        List<string> rightLengthTasks = new List<string>();

        foreach (string task in GetAllTasks())
        {
            if (task.Length >= minTaskLength && task.Length <= maxTaskLength)
                rightLengthTasks.Add(task);
        }

        List<string> result = new List<string>();

        HashSet<int> allreadyAdded = new HashSet<int>();
        int nextIndex;

        while (result.Count < listLength)
        {
            nextIndex = Random.Range(0, rightLengthTasks.Count);

            if (allreadyAdded.Contains(nextIndex))
                nextIndex = Random.Range(0, rightLengthTasks.Count);

            result.Add(rightLengthTasks[nextIndex]);
            allreadyAdded.Add(nextIndex);
        }
        return result;
    }

    private string[] GetAllTasks()
    {
        string[] result = workTaskData.text.Split('\n');
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = result[i].Trim().Replace("  ", " ");
        }

        return result;
    }
}
