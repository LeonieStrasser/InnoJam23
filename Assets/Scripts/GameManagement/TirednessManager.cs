using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TirednessManager : MonoBehaviour
{
    private static TirednessManager instance;
    public static TirednessManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<TirednessManager>();
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
