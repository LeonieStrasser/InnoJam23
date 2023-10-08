using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TaskListGenerator : MonoBehaviour
{
    string DataFolderPath => Application.streamingAssetsPath;

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

        if (rightLengthTasks.Count < 1)
        {
            Debug.LogWarning($"No Task in list fullfilled Requirements({minTaskLength}-{maxTaskLength} Characters)");
            return GenerateList(listLength, Mathf.Max(0, minTaskLength - 1), Mathf.Max(minTaskLength, maxTaskLength + 1));
        }

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
        string[] files = Directory.GetFiles(DataFolderPath, "*.txt");

        if (files.Length < 1) return new string[] { "Missing Files" };

        string[] result = File.ReadAllLines(files[Random.Range(0, files.Length)]);

        for (int i = 0; i < result.Length; i++)
        {
            result[i] = result[i].Trim().Replace("  ", " ");
        }

        return result;
    }

    //[ContextMenu("Sort")]
    //private void Sort()
    //{
    //}
}
