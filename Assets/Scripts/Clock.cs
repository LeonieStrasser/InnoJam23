using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public event Action OnTimerFinished;

    [SerializeField] private SpriteRenderer ClockRenderer;

    [SerializeField] private Transform HourHandle;
    [SerializeField] private Transform MinuteHandle;

    [SerializeField] private float HourHandleStartRotation;
    [SerializeField] private float HourHandleEndRotation;

    [SerializeField] private float MinuteHandleStartRotation;
    [SerializeField] private float MinuteHandleEndRotation;

    private float HourHandleAbsoluteRotationAmount;
    private float HourHandleRotationSign;
    
    private float MinuteHandleAbsoluteRotationAmount;
    private float MinuteHandleRotationSign;

    private float secondsUntilDeathline;
    private float elapsedTimeThisRound;

    private float ClockFillAmount;

    private bool TimerRunning = false;

    public float NormalisedPassedTime => elapsedTimeThisRound / secondsUntilDeathline;

    private void Start()
    {
        HourHandleAbsoluteRotationAmount = Mathf.Abs(HourHandleEndRotation - HourHandleStartRotation);
        HourHandleRotationSign = Mathf.Sign(HourHandleEndRotation - HourHandleStartRotation);
        
        MinuteHandleAbsoluteRotationAmount = Mathf.Abs(MinuteHandleEndRotation - MinuteHandleStartRotation);
        MinuteHandleRotationSign = Mathf.Sign(MinuteHandleEndRotation - MinuteHandleStartRotation);
        
        Debug.Log("HOUR ABS: " + HourHandleAbsoluteRotationAmount);
        Debug.Log("HOUR SIGN: " + HourHandleRotationSign);
    }

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
        ClockFillAmount = elapsedTimeThisRound / secondsUntilDeathline;

        ClockRenderer.material.SetFloat("_FillAmount", ClockFillAmount);

        AudioManager.instance.myTickNoise.UpdateTickingVolume(ClockFillAmount);
        
        UpdateHandlePositions();
    }

    private void UpdateHandlePositions()
    {
        float hourHandleRotation = Mathf.Lerp(0, HourHandleAbsoluteRotationAmount, ClockFillAmount);
        hourHandleRotation = hourHandleRotation * HourHandleRotationSign + HourHandleStartRotation;
        HourHandle.rotation = Quaternion.Euler(0, 0, hourHandleRotation);
        
        float minuteHandleRotation = Mathf.Lerp(0, MinuteHandleAbsoluteRotationAmount, ClockFillAmount);
        minuteHandleRotation = minuteHandleRotation * MinuteHandleRotationSign + MinuteHandleStartRotation;
        MinuteHandle.rotation = Quaternion.Euler(0, 0, minuteHandleRotation);
    }
}
