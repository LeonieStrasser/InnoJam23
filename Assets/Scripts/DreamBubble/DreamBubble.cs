using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

public class DreamBubble : MonoBehaviour
{
    private float Tiredness;
    public event Action<float> OnTirednessChanged;
    public event Action OnMaxTiredness;
    
    [SerializeField] private GameObject SheepPrefab;
    [SerializeField] private Transform SpawnPoint;
    
    [SerializeField] [MinMaxSlider(0f, 20f)]
    private Vector2 SheepSpawnInterval;
    
    [SerializeField] [MinMaxSlider(0f, 10f)]
    private Vector2 SheepSpawnScale;

    [SerializeField] [MinMaxSlider(0f, 15f)]
    private Vector2 SheepSpawnVelocity;

    [SerializeField] private float MaxSheepHeight = 2.4f;
    [SerializeField] private float FloorOffset = 2.4f;

    private List<Sheep> AllSheep = new List<Sheep>();

    private float SheepSpawnTimer;
    private float CurrentSpawnInterval;

    private void Start()
    {
        SetNewSpawnInterval(false);
    }

    private void Update()
    {
        SheepSpawnTimer += Time.deltaTime;

        if (SheepSpawnTimer >= CurrentSpawnInterval)
        {
            SpawnSheep();
            SetNewSpawnInterval(true);
            SheepSpawnTimer = 0;
        }
    }

    private void SetNewSpawnInterval(bool _increaseFrequency)
    {
        if (_increaseFrequency && SheepSpawnInterval.x > 1)
        {
            SheepSpawnInterval -= Vector2.one;
        }

        CurrentSpawnInterval = Random.Range(SheepSpawnInterval.x, SheepSpawnInterval.y);
    }
    
    [Button]
    private void SpawnSheep()
    {
        GameObject newSheep = Instantiate(SheepPrefab, SpawnPoint.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))), gameObject.transform);
        newSheep.transform.localScale = Vector3.one * Random.Range(SheepSpawnScale.x, SheepSpawnScale.y);
        Rigidbody2D newSheepRb = newSheep.GetComponent<Rigidbody2D>();
        
        newSheepRb.AddForce(Vector2.right * Random.Range(SheepSpawnVelocity.x, SheepSpawnVelocity.y), ForceMode2D.Impulse);
        
        AllSheep.Add(newSheep.GetComponent<Sheep>());
    }

    public void DestroySheep(Sheep _sheep)
    {
        AllSheep.Remove(_sheep);
        
        _sheep.gameObject.SetActive(false);
        // play disappear animations or something like that here
        Destroy(_sheep.gameObject, 0.5f);
    }

    [Button]
    public void Reset()
    {
        foreach (var sheep in AllSheep)
            Destroy(sheep.gameObject);
        
        AllSheep.Clear();

        Tiredness = 0;
    }

    public void UpdateTiredness()
    {
        float topHeight = 0;
        
        foreach (Sheep sheep in AllSheep)
        {
            if(sheep.GetComponent<Rigidbody2D>().velocity.magnitude > 0.5f)
                continue;
            
            if (sheep.transform.localPosition.y + FloorOffset > topHeight)
                topHeight = sheep.transform.localPosition.y + FloorOffset;
        }
        
        Tiredness = Mathf.Clamp(topHeight / (MaxSheepHeight + FloorOffset), 0f, 1f);
        
        OnTirednessChanged?.Invoke(Tiredness);
        
        if(Tiredness >= 1f)
            OnMaxTiredness?.Invoke();
    }
}
