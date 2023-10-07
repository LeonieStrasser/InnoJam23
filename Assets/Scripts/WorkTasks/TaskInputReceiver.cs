using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInputReceiver : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnPressedKey;
    public UnityEngine.Events.UnityEvent OnRemovedLastKey;
    public UnityEngine.Events.UnityEvent OnPressedSubmit;
    public event System.Action<string> OnPressedSubmitWithText;

    [SerializeField] TMPro.TMP_Text providedInputVisual;

    string playerTypedKeys;

    private static TaskInputReceiver instance;
    public static TaskInputReceiver Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TaskInputReceiver>();
            return instance;
        }
    }

    bool textCursorVisible;
    float textCursorTime = 0;
    [SerializeField, Min(0.1f)] float textCursorIntervalSecond = 0.5f;

    private bool taskInputBlocked;

    private void Awake()
    {
        instance = this;

        UpdateVisual();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (taskInputBlocked) return;

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                SubmitInput();
            else if (Input.GetKeyDown(KeyCode.Backspace))
                RemoveLastKey();
            else
            {
                if (Input.inputString.Length > 0)
                    AddKey(Input.inputString[Input.inputString.Length - 1]);
            }
        }

        UpdateCursorVisibility();
    }

    private void SubmitInput()
    {
        Debug.Log("Player typed " + playerTypedKeys);

        OnPressedSubmit?.Invoke();
        OnPressedSubmitWithText?.Invoke(playerTypedKeys);

        playerTypedKeys = "";
        UpdateVisual();
    }

    private void AddKey(char addedKey)
    {
        playerTypedKeys += addedKey;

        OnPressedKey?.Invoke();
        UpdateVisual();
    }

    private void RemoveLastKey()
    {
        if (playerTypedKeys.Length == 0)
            return;

        playerTypedKeys = playerTypedKeys.Substring(0, playerTypedKeys.Length - 1);

        OnRemovedLastKey?.Invoke();
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        if (textCursorVisible)
            providedInputVisual.text = playerTypedKeys + "_";
        else
            providedInputVisual.text = playerTypedKeys;
    }

    private void UpdateCursorVisibility()
    {
        textCursorTime += Time.deltaTime;
        if (textCursorTime > textCursorIntervalSecond)
        {
            textCursorVisible = !textCursorVisible;
            UpdateVisual();
            textCursorTime -= textCursorIntervalSecond;
        }
    }

    public void SetInputBlocked(bool isBlocked)
    {
        taskInputBlocked = isBlocked;
    }
}
