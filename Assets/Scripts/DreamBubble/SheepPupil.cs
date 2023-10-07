using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepPupil : MonoBehaviour
{
    [SerializeField] private float MaxMovementRadius;

    private Vector3 StartOffset;

    private void Start()
    {
        StartOffset = transform.localPosition;
    }

    void Update()
    {
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursorWorldPos = new Vector3(cursorWorldPos.x, cursorWorldPos.y, 0);

        Vector3 directionToCursor = (cursorWorldPos - transform.parent.position).normalized;

        transform.localPosition = transform.InverseTransformDirection(directionToCursor) * MaxMovementRadius + StartOffset;
    }
}
