using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaUrchin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().HitEnemy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().LeaveEnemy();
        }
    }
}
