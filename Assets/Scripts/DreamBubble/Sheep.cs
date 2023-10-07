using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Sheep : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SheepSprite;

    private DreamBubble Bubble;

    private Vector3 SpriteStartScale;

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
        SheepSprite.transform.DOScale(SpriteStartScale * 1.2f, 0.2f);
    }

    private void OnMouseExit()
    {
        SheepSprite.transform.DOScale(SpriteStartScale, 0.2f);
    }

    private void OnMouseDown()
    {
        SheepSprite.transform.DOKill();
        Bubble.UpdateTiredness();
        Bubble.DestroySheep(this);
    }
}
