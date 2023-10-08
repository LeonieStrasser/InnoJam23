using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class UIVisualEffect : MonoBehaviour
{
    [SerializeField] private Image VignetteRenderer;

    [SerializeField] private float MaxVignetteStrength = 0.8f;

    private float TargetedVignetteStrength;

    private float CurrentVignetteStrength;

    private DreamBubble Bubble;
    
    private void Start()
    {
        Bubble = FindObjectOfType<DreamBubble>();
        Bubble.OnTirednessChanged += UpdateVignette;
    }

    private void Update()
    {
        if (Mathf.Abs(TargetedVignetteStrength - CurrentVignetteStrength) < 0.05f)
            return;

        CurrentVignetteStrength = Mathf.Lerp(CurrentVignetteStrength, TargetedVignetteStrength, 3.5f * Time.deltaTime);
        
        var vignetteRendererColor = VignetteRenderer.color;
        vignetteRendererColor.a = CurrentVignetteStrength;
        VignetteRenderer.color = vignetteRendererColor;
    }

    private void UpdateVignette(float _tiredness)
    {
        TargetedVignetteStrength = Mathf.Lerp(0, MaxVignetteStrength, _tiredness);
    }

    private void OnDestroy()
    {
        Bubble.OnTirednessChanged -= UpdateVignette;
    }
}
