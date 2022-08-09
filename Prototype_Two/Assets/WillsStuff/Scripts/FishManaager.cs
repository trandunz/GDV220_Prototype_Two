using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManaager : MonoBehaviour
{
    int[] PossibleDirections = new int[2];

    private void Start()
    {
        PossibleDirections[0] = 1;
        PossibleDirections[1] = -1;
        int randomDirection = PossibleDirections[Random.Range(0, 2)];
        foreach (var fish in GetComponentsInChildren<FishBob>())
        {
            fish.SetXDirection(randomDirection);
        }
    }
}
