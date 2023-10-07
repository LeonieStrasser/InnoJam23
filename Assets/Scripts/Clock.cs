using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public event Action OnTimerFinished;

    [SerializeField] private SpriteRenderer ClockRenderer;

    private float secondsUntilDeathline;
    private float elapsedTimeThisRound;

    private bool TimerRunning = false;

    void Update()
    {
        if (!TimerRunning)
            return;

        elapsedTimeThisRound += Time.deltaTime;

        if (elapsedTimeThisRound >= secondsUntilDeathline)
            TimerFinished();
        else
            UpdateClockVisuals();
    }

    public void StartTimer(float _duration)
    {
        Debug.Log("Set Start Timer to " + _duration);

        secondsUntilDeathline = _duration;
        elapsedTimeThisRound = 0;

        TimerRunning = true;
    }

    public void Continue()
    {
        TimerRunning = (elapsedTimeThisRound < secondsUntilDeathline);
    }

    public void Pause()
    {
        TimerRunning = false;
    }

    private void TimerFinished()
    {
        Debug.Log("Timer is Finished");

        TimerRunning = false;
        elapsedTimeThisRound = secondsUntilDeathline;

        UpdateClockVisuals();

        OnTimerFinished?.Invoke();
    }

    private void UpdateClockVisuals()
    {
        float fillAmount = elapsedTimeThisRound / secondsUntilDeathline;

        ClockRenderer.material.SetFloat("_FillAmount", 1 - fillAmount);

        AudioManager.instance.myTickNoise.UpdateTickingVolume(fillAmount);
    }
}
