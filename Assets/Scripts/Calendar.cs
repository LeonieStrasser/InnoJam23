using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    [SerializeField, Min(0)] int firstDayOffset = 0;

    [SerializeField] private GameObject CheckmarkPrefab;
    [SerializeField] private GameObject TargetCirclePrefab;

    [SerializeField] private Transform CalendarCheckmarkParent;

    private GameObject InstantiatedTargetCircle;
    private List<GameObject> InstantiatedCheckmarks = new List<GameObject>();

    private List<Transform> CheckmarkPositions = new List<Transform>();

    private static Calendar instance;
    public static Calendar Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<Calendar>();
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

        _targetDay += firstDayOffset;

        InstantiatedTargetCircle = Instantiate(TargetCirclePrefab, CheckmarkPositions[_targetDay].position, Quaternion.identity, CheckmarkPositions[_targetDay]);
    }

    [Button]
    public void SetCheckmarks(int _daysFinished)
    {
        RemoveCheckmarks();

        for (int i = firstDayOffset; i < _daysFinished + firstDayOffset; i++)
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
        if (InstantiatedTargetCircle)
            Destroy(InstantiatedTargetCircle);
    }
}
