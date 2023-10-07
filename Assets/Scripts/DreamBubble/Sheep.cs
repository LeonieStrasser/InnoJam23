using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sheep : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SheepSprite;
    [SerializeField] private Sprite SheepBlack;
    [SerializeField] private float BlackSheepScaleUpFactor;
    [SerializeField] private float BlackSheepScaleUpDuration;
    [SerializeField] private float BlackSheepBigDuration;
    [SerializeField] private GameObject sheepPuffVFX;

    private DreamBubble Bubble;

    private Vector3 SpriteStartScale;

    private bool IsBlack;

    private bool ImmuneToMouse;

    private void Start()
    {
        Bubble = transform.parent.GetComponent<DreamBubble>();
        SpriteStartScale = SheepSprite.transform.localScale;
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
        
        SheepSprite.transform.DOScale(SpriteStartScale, 0.2f);
    }

    private void OnMouseDown()
    {
        if (ImmuneToMouse)
            return;
        
        AudioManager.instance.PlayStackable("Sheep");

        if (IsBlack)
        {
            StartCoroutine(BlackSheepClick());
            return;
        }
        
        Instantiate(sheepPuffVFX, transform.position, Quaternion.identity, null);
        Remove();
    }

    private IEnumerator BlackSheepClick()
    {
        ImmuneToMouse = true;

        transform.position -= new Vector3(0, 0, 0.15f);
        
        SheepSprite.transform.DOKill();
        SheepSprite.transform.DOScale(SpriteStartScale * BlackSheepScaleUpFactor, BlackSheepScaleUpDuration);

        yield return new WaitForSeconds(BlackSheepScaleUpDuration + BlackSheepBigDuration);
        
        Instantiate(sheepPuffVFX, transform.position, Quaternion.identity, null);
        
        Remove();
    }

    public void MakeBlack()
    {
        SheepSprite.sprite = SheepBlack;

        IsBlack = true;
    }

    private void Remove()
    {
        SheepSprite.transform.DOKill();
        Bubble.UpdateTiredness();
        Bubble.DestroySheep(this);
    }
}
