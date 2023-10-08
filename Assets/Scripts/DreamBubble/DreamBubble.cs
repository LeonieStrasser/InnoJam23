using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    [MinMaxSlider(0f, 20f)]
    private Vector2 SheepSpawnInterval;

    [SerializeField]
    [MinMaxSlider(0f, 10f)]
    private Vector2 SheepSpawnScale;

    [SerializeField]
    [MinMaxSlider(-15f, 15f)]
    private Vector2 SheepSpawnVelocity;

    [SerializeField] private float BlackSheepProbability = 0.1f;

    [SerializeField] private Transform MaxHeightMarker;
    [SerializeField] private Transform FloorHeightMarker;

    private List<Sheep> AllSheep = new List<Sheep>();

    private float SheepSpawnTimer;
    private float CurrentSpawnInterval;

    private static DreamBubble instance;
    public static DreamBubble Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<DreamBubble>();
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

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

        Sheep newSheepComponent = newSheep.GetComponent<Sheep>();
        AllSheep.Add(newSheepComponent);

        if (ShouldSpawnBlackSheep())
            newSheepComponent.MakeBlack();

        newSheep.transform.localScale = Vector3.one * Random.Range(SheepSpawnScale.x, SheepSpawnScale.y);
        Rigidbody2D newSheepRb = newSheep.GetComponent<Rigidbody2D>();

        newSheepRb.AddForce(Vector2.right * Random.Range(SheepSpawnVelocity.x, SheepSpawnVelocity.y), ForceMode2D.Impulse);
    }

    public void DestroySheep(Sheep _sheep)
    {
        AllSheep.Remove(_sheep);

        _sheep.gameObject.SetActive(false);
        // play disappear animations or something like that here
        Destroy(_sheep.gameObject, 0.5f);
    }

    [Button]
    public void RemoveSheep() => RemoveSheep(0, 0);

    public void RemoveSheep(float normalisedKeptWhiteSheep, float normalisedKeptBlackSheep)
    {
        CurbSheep(normalisedKeptWhiteSheep, isBlack: false);
        CurbSheep(normalisedKeptBlackSheep, isBlack: true);

        UpdateTiredness();
        OnTirednessChanged?.Invoke(Tiredness);
    }

    public void UpdateTiredness()
    {
        float topHeight = -200;

        if (AllSheep.Count == 0)
        {
            Tiredness = 0;
            return;
        }

        foreach (Sheep sheep in AllSheep)
        {
            if (sheep.GetComponent<Rigidbody2D>().velocity.magnitude > 0.2f)
                continue;

            if (sheep.Height > topHeight)
                topHeight = sheep.Height;
        }

        Tiredness = Mathf.InverseLerp(FloorHeightMarker.localPosition.y, MaxHeightMarker.localPosition.y, topHeight);

        OnTirednessChanged?.Invoke(Tiredness);

        if (Tiredness >= 1f)
            OnMaxTiredness?.Invoke();
    }

    private bool ShouldSpawnBlackSheep()
    {
        float random = Random.Range(0f, 0.9f);

        return random <= BlackSheepProbability;
    }

    private void CurbSheep(float normalisedKeptSheep, bool isBlack)
    {
        List<Sheep> monoColorSheep = new List<Sheep>(AllSheep.Where((x) => (x.IsBlack == isBlack)));

        float alreadyKeptNormalised = 0;
        int counter = 0;
        for (int i = 0; i < monoColorSheep.Count(); i++)
        {
            if (alreadyKeptNormalised < normalisedKeptSheep)
            {
                counter++;
                alreadyKeptNormalised = (float)counter / (float)monoColorSheep.Count();

                //Debug.Log($"Sheep({counter},black:{isBlack})=> {alreadyKeptNormalised}");
            }
            else
            {
                monoColorSheep[i].isDying = true;
                monoColorSheep[i].Remove();
            }
        }

        AllSheep = new List<Sheep>(AllSheep.Where((x) => !x.isDying));
    }
}
