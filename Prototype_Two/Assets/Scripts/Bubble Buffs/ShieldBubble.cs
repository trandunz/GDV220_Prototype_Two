using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBubble : MonoBehaviour
{
    SwimController m_Player;
    private void Start()
    {
        m_Player = GetComponentInParent<SwimController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Enemy")
        {
            m_Player.BubbleShieldHit();
            Destroy(gameObject);
        }
    }
}
