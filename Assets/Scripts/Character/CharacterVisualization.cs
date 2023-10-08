using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisualization : MonoBehaviour
{
    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer FaceRenderer;
    [SerializeField] private SpriteRenderer HairRenderer;
    [SerializeField] private SpriteRenderer HeadRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite FaceAwake;
    [SerializeField] private Sprite FaceTired;
    [SerializeField] private Sprite FaceVeryTired;

    [SerializeField] private Sprite Hair;
    [SerializeField] private Sprite HairMessy;

    [SerializeField] private Sprite Head;
    [SerializeField] private Sprite HeadPale;

    [Header("Animation")]
    [SerializeField] private Animator anim;

    [Header("Other Settings")]
    [SerializeField] private float FaceChangeCooldownDuration = 1f;

    private enum FaceState
    {
        Awake, Tired, VeryTired
    }

    private FaceState CurrentFaceState = FaceState.Awake;

    private DreamBubble Bubble;

    private bool FaceChangeAllowed = true;

    private void Start()
    {
        Bubble = FindObjectOfType<DreamBubble>();
        Bubble.OnTirednessChanged += UpdateFace;
    }

    public void UpdateFace(float _tiredness)
    {
        if (!FaceChangeAllowed)
            return;

        if (_tiredness < 0.33f)
        {
            ChangeFaceState(FaceState.Awake);
            SetAnimationState(_tiredness);
        }


        else if (_tiredness > 0.33f && _tiredness < 0.66f)
        {
            ChangeFaceState(FaceState.Tired);
            SetAnimationState(_tiredness);
        }
        else
        {
            ChangeFaceState(FaceState.VeryTired);
            SetAnimationState(_tiredness);
        }
    }

    private void ChangeFaceState(FaceState _newFaceState)
    {
        if (_newFaceState == CurrentFaceState)
            return;

        StartCoroutine(FaceChangeCooldown());

        switch (_newFaceState)
        {
            case FaceState.Awake:
                FaceRenderer.sprite = FaceAwake;
                break;
            case FaceState.Tired:
                FaceRenderer.sprite = FaceTired;
                break;
            case FaceState.VeryTired:
                FaceRenderer.sprite = FaceVeryTired;
                break;
        }
    }

    IEnumerator FaceChangeCooldown()
    {
        FaceChangeAllowed = false;

        yield return new WaitForSeconds(FaceChangeCooldownDuration);

        FaceChangeAllowed = true;
    }

    private void OnDestroy()
    {
        Bubble.OnTirednessChanged -= UpdateFace;
    }

    private void SetAnimationState(float tiredness)
    {
        anim.SetFloat("tired", tiredness);
    }
}
