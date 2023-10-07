using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskInputReceiver : MonoBehaviour
{
    public event System.Action<char> OnPressedKey;
    public event System.Action OnRemovedLastKey;
    public event System.Action<string> OnPressedSubmit;

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

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void SubmitInput()
    {
        Debug.Log("Player typed " + playerTypedKeys);

        OnPressedSubmit?.Invoke(playerTypedKeys);

        playerTypedKeys = "";
        UpdateVisual();
    }

    private void AddKey(char addedKey)
    {
        playerTypedKeys += addedKey;

        UpdateVisual();
        OnPressedKey?.Invoke(addedKey);
    }

    private void RemoveLastKey()
    {
        if (playerTypedKeys.Length == 0)
            return;

        playerTypedKeys = playerTypedKeys.Substring(0, playerTypedKeys.Length - 1);

        UpdateVisual();
        OnRemovedLastKey?.Invoke();
    }

    private void UpdateVisual()
    {
        providedInputVisual.text = playerTypedKeys;
    }
}
