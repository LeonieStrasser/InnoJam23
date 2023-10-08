using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListGenerator : MonoBehaviour
{
    string DataFilePath => Application.streamingAssetsPath + "/WorkTaskData.txt";

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

        if (rightLengthTasks.Count < 1)
        {
            Debug.LogError($"No Task in list fullfilled Requirements({minTaskLength}-{maxTaskLength} Characters)");
            return result;
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
         
      
        string[] result = System.IO.File.ReadAllLines(DataFilePath);//workTaskData.text.Split('\n');
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
