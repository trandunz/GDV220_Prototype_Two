using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    SwimController m_SwimController;

    private void Start()
    {
        m_SwimController = GetComponentInParent<SwimController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Oxygem")
        {
            m_SwimController.PickupOxygem(other);
        }
        if (other.gameObject.tag is "Bubble")
        {
            m_SwimController.PickupBubble(other);
        }
    }
}
