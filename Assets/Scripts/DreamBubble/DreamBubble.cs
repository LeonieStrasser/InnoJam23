using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamBubble : MonoBehaviour
{
    [SerializeField] private GameObject SheepPrefab;
    [SerializeField] private Transform SpawnPoint;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnSheep()
    {
        GameObject newSpeep = Instantiate(SheepPrefab, SpawnPoint.position, Quaternion.identity, gameObject.transform);
    }
}
