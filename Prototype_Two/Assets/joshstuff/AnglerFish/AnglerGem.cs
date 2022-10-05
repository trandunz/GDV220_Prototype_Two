using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerGem : MonoBehaviour
{
    [SerializeField] AnglerFish fish;

    private void FixedUpdate()
    {
    }

    private void Update()
    {
        float spin = 40.0f * Time.deltaTime;
        transform.Rotate(new Vector3(0.0f, spin, 0.0f), Space.Self);
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
