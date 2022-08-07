using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Player")
        {
            GemManager.instance.AddGems(1);
            Destroy(gameObject);
        }
    }
}
