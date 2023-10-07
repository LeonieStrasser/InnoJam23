using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public event Action OnTimerFinished;

    [SerializeField] private SpriteRenderer ClockRenderer;
    
    private float CurrentDuration;
    private float CurrentTimer;

    private bool TimerRunning;

    void Update()
    {
        if (!TimerRunning)
            return;

        CurrentTimer += Time.deltaTime;
        
        UpdateClockVisuals();

        if (CurrentTimer >= CurrentDuration)
        {
            TimerFinished();
        }
    }

    public void StartTimer(float _duration)
    {
        CurrentDuration = _duration;

        TimerRunning = true;
    }

    private void TimerFinished()
    {
        OnTimerFinished?.Invoke();
    }

    private void UpdateClockVisuals()
    {
        float fillAmount = 1 - CurrentTimer / CurrentDuration;
        
        ClockRenderer.material.SetFloat("_FillAmount", fillAmount);
    }
}
