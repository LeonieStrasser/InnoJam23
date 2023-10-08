using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderOrder : MonoBehaviour
{
    public int sortingOrder = 0;

    private void Awake()
    {
        gameObject.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
    }
}
