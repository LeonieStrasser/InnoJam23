using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CoffeeFeedback : MonoBehaviour
{

    public GameObject[] coffeeToHide;
    public GameObject DespawnFX;

    [Button]
    public void DrinkCoffee()
    {
        foreach (var item in coffeeToHide)
        {
            item.SetActive(false);

        }

        Instantiate(DespawnFX, transform.position, Quaternion.identity, null);
    }

    [Button]
    public void ResetCoffee()
    {
        foreach (var item in coffeeToHide)
        {
            item.SetActive(true);

        }
    }
}
