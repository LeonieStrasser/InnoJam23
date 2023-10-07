using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeonRohr : MonoBehaviour
{
    public SpriteRenderer[] sprites; // Das Material, das den "AlertState" hat.
  
    private float currentAlertState = 0.0f;

   

    public void UpdateNeonColor(float lerpValue)
    {
        // Lerp den "AlertState" von seinem aktuellen Wert zum Zielwert (1.0) über die Zeit.
        currentAlertState = Mathf.Lerp(currentAlertState, 1.0f, lerpValue);

        // Setzen Sie den neuen Wert des "AlertState" im Material.
        foreach (var item in sprites)
        {
            item.material.SetFloat("AlertState", currentAlertState);
        }

        
    }
}
