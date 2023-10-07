using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskListManager : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnFullfilledTask;
    public UnityEngine.Events.UnityEvent OnFailedTask;
    public UnityEngine.Events.UnityEvent OnFinishedRequiredTasks;

    [SerializeField] TMPro.TMP_Text taskListVisual;
    [SerializeField] TaskListGenerator listGenerator;
    List<string> taskList;
    public bool AreRequiredWorkTaskFinished => taskList.Count == 0;

    private static TaskListManager instance;
    public static TaskListManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TaskListManager>();
            return instance;
        }
    }


    private void Awake()
    {
        instance = this;

        TaskInputReceiver.Instance.OnPressedSubmitWithText += SubmittedTask;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;

        if (TaskInputReceiver.Instance)
            TaskInputReceiver.Instance.OnPressedSubmitWithText -= SubmittedTask;
    }

    public void InitialiseList(int taskListSize,int minTaskLength, int maxTaskLength)
    {
        taskList = listGenerator.GenerateList(taskListSize, minTaskLength, maxTaskLength);
        UpdateTaskListVisual();
    }

    private void SubmittedTask(string submittedText)
    {
        if (submittedText.Length < 1) return;

        Debug.Log($"Player submitted Task: '{submittedText}'");

        foreach (string task in taskList)
        {
            if (submittedText.CompareTo(task) == 0)
            {
                FullfilledTask(task);
                return;
            }
        }
        FailedTask(submittedText);
    }

    private void FullfilledTask(string task)
    {
        Debug.Log("Fullfilled " + task);
        taskList.Remove(task);

        OnFullfilledTask?.Invoke();

        UpdateTaskListVisual();

        if (AreRequiredWorkTaskFinished)
        {
            GameManager.Instance.SuccessfullDeathline();
            OnFinishedRequiredTasks?.Invoke();
        }
    }

    private void FailedTask(string sublittedText)
    {
        OnFailedTask?.Invoke();
    }

    private void UpdateTaskListVisual()
    {
        string taskText = "";
        foreach (string task in taskList)
        {
            taskText += "\n" + task;
        }

        taskListVisual.text = taskText.TrimStart();
    }
}
