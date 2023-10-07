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

    public enum UIType
    {
        Start, TimeGO, SleepGO, Win, Quit
    }

    private GameObject CurrentActiveUI;

    public void ShowUI(UIType _type)
    {
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

    public void RestartButtonHit()
    {
        // restart game here
    }
    
    public void ResignButtonHit()
    {
        // restart game here
    }
    
    public void NextRoundButtonHit()
    {
        // restart game here
    }
}
