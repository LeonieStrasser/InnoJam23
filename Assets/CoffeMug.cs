using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeMug : MonoBehaviour
{
    [SerializeField] private SpriteRenderer scaledSprite;
    [SerializeField, Min(0.5f)] float scaleIncreaseFactor = 1.05f;
    private Vector3 spriteStartScale;

    // Start is called before the first frame update
    void Start()
    {
        spriteStartScale = scaledSprite.transform.localScale;
    }

    private void OnMouseOver()
    {
        scaledSprite.transform.DOScale(spriteStartScale * scaleIncreaseFactor, 0.2f);
    }

    private void OnMouseExit()
    {
        scaledSprite.transform.DOScale(scaleIncreaseFactor, 0.2f);
    }

    private void OnMouseDown()
    {
        Debug.Log("Coffee break");
        RoundManager.Instance.Pause();
    }

    private void OnMouseUp()
    {
        Debug.Log("Coffee break over");
        RoundManager.Instance.Continue();
    }
}
