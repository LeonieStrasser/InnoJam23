using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEditor;

public class Sheep : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SheepSprite;
    [SerializeField] private Sprite SheepBlack;
    
    [SerializeField] private float ClickHoldDuration;
    
    [SerializeField] private float BlackSheepScaleUpFactor;
    [SerializeField] private float BlackSheepScaleUpDuration;
    [SerializeField] private float BlackSheepBigDuration;
    
    [SerializeField] private GameObject sheepPuffVFX;
    [SerializeField] private GameObject sheepBlackPuffVFX;

    private DreamBubble Bubble;

    private Vector3 SpriteStartScale;

    private bool IsBlack;

    private bool ImmuneToMouse;

    private bool ClickHoldActive;

    private float CurrentClickHoldTimer;

    private void Start()
    {
        Bubble = transform.parent.GetComponent<DreamBubble>();
        SpriteStartScale = SheepSprite.transform.localScale;
    }

    private void Update()
    {
        if (!ClickHoldActive)
            return;

        CurrentClickHoldTimer += Time.deltaTime;

        if (CurrentClickHoldTimer >= ClickHoldDuration)
        {
            OnClickedLongEnough();
            ResetClickHold();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sheep"))
            Bubble.UpdateTiredness();
    }

    private void OnMouseOver()
    {
        if (ImmuneToMouse)
            return;
        
        SheepSprite.transform.DOScale(SpriteStartScale * 1.2f, 0.2f);
    }

    private void OnMouseExit()
    {
        if (ImmuneToMouse)
            return;
        
        ResetClickHold();
        
        SheepSprite.transform.DOScale(SpriteStartScale, 0.2f);
    }

    private void OnMouseDown()
    {
        if (ImmuneToMouse)
            return;

        ClickHoldActive = true;

        SheepSprite.transform.DOShakePosition(3f, 0.2f, 10, 90f, false, false, ShakeRandomnessMode.Full);
    }

    private void OnMouseUp()
    {
        SheepSprite.transform.DOKill();
        
        ResetClickHold();
    }

    private IEnumerator BlackSheepScaleUp()
    {
        ImmuneToMouse = true;

        transform.position -= new Vector3(0, 0, 0.15f);
        
        SheepSprite.transform.DOKill();
        SheepSprite.transform.DOScale(SpriteStartScale * BlackSheepScaleUpFactor, BlackSheepScaleUpDuration);

        yield return new WaitForSeconds(BlackSheepScaleUpDuration + BlackSheepBigDuration);
        
        Instantiate(sheepBlackPuffVFX, transform.position, Quaternion.identity, null);
        
        Remove();
    }

    private void ResetClickHold()
    {
        ClickHoldActive = false;
        CurrentClickHoldTimer = 0;
    }

    public void MakeBlack()
    {
        SheepSprite.sprite = SheepBlack;

        IsBlack = true;
    }

    private void OnClickedLongEnough()
    {
        AudioManager.instance.PlayStackable("Sheep");
        AudioManager.instance.PlayStackable("SheepWind");

        if (IsBlack)
        {
            StartCoroutine(BlackSheepScaleUp());
            return;
        }
        
        Instantiate(sheepPuffVFX, transform.position, Quaternion.identity, null);
        Remove();
    }

    private void Remove()
    {
        SheepSprite.transform.DOKill();
        Bubble.UpdateTiredness();
        Bubble.DestroySheep(this);
    }
}
