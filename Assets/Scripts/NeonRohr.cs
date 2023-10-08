using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonRohr : MonoBehaviour
{
    public SpriteRenderer[] sprites; // Das Material, das den "AlertState" hat.
  
    private float currentAlertState = 0.0f;

   

    public void Update()
    {
        // Lerp den "AlertState" von seinem aktuellen Wert zum Zielwert (1.0) über die Zeit.
        currentAlertState = Mathf.Lerp(0, 1.0f, RoundManager.Instance.NormalisedPassedTime);
        //Debug.Log(currentAlertState);

        // Setzen Sie den neuen Wert des "AlertState" im Material.
        foreach (var item in sprites)
        {
            item.material.SetFloat("_AlertState", currentAlertState);
        }

        
    }
}
