using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeMug : MonoBehaviour
{
    [SerializeField] private SpriteRenderer scaledSprite;
    [SerializeField, Min(0.5f)] float scaleIncreaseFactor = 1.05f;
    private Vector3 spriteStartScale;

    [SerializeField, Range(0f, 1f)] float removedWhiteSheep = 0.2f;
    [SerializeField, Range(0f, 1f)] float removedBlackSheep = 1f;

    public bool hasBeenUsed;
    [SerializeField] private CoffeeFeedback myFeedback;


    private static CoffeMug instance;
    public static CoffeMug Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CoffeMug>();
            return instance;
        }
    }


    private void Awake()
    {
        instance = this;
        
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteStartScale = scaledSprite.transform.localScale;
    }

    private void OnMouseOver()
    {
        if (hasBeenUsed) return;

        scaledSprite.transform.DOScale(spriteStartScale * scaleIncreaseFactor, 0.2f);
    }

    private void OnMouseExit()
    {
        scaledSprite.transform.DOScale(spriteStartScale, 0.2f);
    }

    private void OnMouseDown()
    {
        if (hasBeenUsed) return;

        Debug.Log("Drink COFFEE!");
        DreamBubble.Instance.RemoveSheep(1f - removedWhiteSheep, 1f - removedBlackSheep);

        hasBeenUsed = true;
        myFeedback.DrinkCoffee();
    }

    public void ResetCoffee()
    {
        hasBeenUsed = false;
        myFeedback.ResetCoffee();
    }
}
