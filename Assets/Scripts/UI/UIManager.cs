using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject TimeGameOverScreen;
    [SerializeField] private GameObject SleepGameOverScreen;
    [SerializeField] private GameObject RoundWonScreen;
    [SerializeField] private GameObject QuitScreen;
    
    [SerializeField] private GameObject ScreenBackround;
    
    private RoundManager RoundManagement;

    public enum UIType
    {
        Start, TimeGO, SleepGO, Win, Quit
    }

    private UIType CurrentUIType;
    private UIType PreviousUIType;

    private GameObject CurrentActiveUI;

    private void Start()
    {
        RoundManagement = FindObjectOfType<RoundManager>();
    }

    public void ShowUI(UIType _type)
    {
        CurrentUIType = _type;
        
        if(CurrentActiveUI)
            CurrentActiveUI.SetActive(false);
        
        ScreenBackround.SetActive(true);
        
        switch (_type)
        {
            case UIType.Start:
                CurrentActiveUI = StartScreen;
                StartScreen.SetActive(true);
                break;
            case UIType.TimeGO:
                CurrentActiveUI = TimeGameOverScreen;
                TimeGameOverScreen.SetActive(true);
                break;
            case UIType.SleepGO:
                CurrentActiveUI = SleepGameOverScreen;
                SleepGameOverScreen.SetActive(true);
                break;
            case UIType.Win:
                CurrentActiveUI = RoundWonScreen;
                RoundWonScreen.SetActive(true);
                break;
            case UIType.Quit:
                CurrentActiveUI = QuitScreen;
                ScreenBackround.SetActive(false);
                QuitScreen.SetActive(true);
                break;
        }
    }

    public void HideUI()
    {
        CurrentActiveUI.SetActive(false);
        ScreenBackround.SetActive(false);
    }
    
    public void StartButtonHit()
    {
        RoundManagement.StartFirstRound();
    }

    public void RestartButtonHit()
    {
        RoundManagement.StartFirstRound();
    }
    
    public void ResignButtonHit()
    {
        PreviousUIType = CurrentUIType;
        ShowUI(UIType.Quit);
    }
    
    public void ResignAbort()
    {
        ShowUI(PreviousUIType);
    }
    
    public void ResignConfirm()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        Application.Quit();
        #endif
    }
    
    public void NextRoundButtonHit()
    {
        RoundManagement.StartNextRound();
    }
}
