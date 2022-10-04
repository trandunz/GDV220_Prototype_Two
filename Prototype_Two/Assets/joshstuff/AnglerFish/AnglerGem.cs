using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerGem : MonoBehaviour
{
    [SerializeField] AnglerFish fish;

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            fish.Attack();
        }

        //if (other.gameObject.CompareTag("AnglerFish"))
        //{
        //    transform.SetParent(fish.transform);
        //}
    }
}
