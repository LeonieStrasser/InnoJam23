using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    [SerializeField] private GameObject CheckmarkPrefab;
    [SerializeField] private GameObject TargetCirclePrefab;
    
    [SerializeField] private Transform CalendarCheckmarkParent;

    private GameObject InstantiatedTargetCircle;
    private List<GameObject> InstantiatedCheckmarks = new List<GameObject>();
    
    private List<Transform> CheckmarkPositions = new List<Transform>();
    
    void Start()
    {
        FillCheckmarkPositionList();
    }

    private void FillCheckmarkPositionList()
    {
        for (int i = 0; i < CalendarCheckmarkParent.childCount; i++)
        {
            CheckmarkPositions.Add(CalendarCheckmarkParent.GetChild(i));
        }
    }
    
    [Button]
    public void SetTargetDay(int _targetDay)
    {
        RemoveTargetDay();
        
        InstantiatedTargetCircle = Instantiate(TargetCirclePrefab, CheckmarkPositions[_targetDay].position, Quaternion.identity, CheckmarkPositions[_targetDay]);
    }
    
    [Button]
    public void SetCheckmarks(int _daysFinished)
    {
        RemoveCheckmarks();
        
        for (int i = 0; i < _daysFinished; i++)
        {
            InstantiatedCheckmarks.Add(Instantiate(CheckmarkPrefab, CheckmarkPositions[i].position, Quaternion.identity, CheckmarkPositions[i]));
        }
    }
    
    [Button]
    public void RemoveCheckmarks()
    {
        foreach (var checkmark in InstantiatedCheckmarks)
        {
            Destroy(checkmark);
        }
        
        InstantiatedCheckmarks.Clear();
    }
    
    [Button]
    public void RemoveTargetDay()
    {
        if(InstantiatedTargetCircle)
            Destroy(InstantiatedTargetCircle);
    }
}
