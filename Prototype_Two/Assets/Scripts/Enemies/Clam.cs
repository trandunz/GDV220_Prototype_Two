using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clam : MonoBehaviour
{
    [SerializeField] ParticleSystem m_SuctionParticle;

    [Header("Suction Settings")]
    [SerializeField] float m_SuctionStrength = 100.0f;
    [SerializeField] float m_SuctionAngle = 15.0f;
    [SerializeField] float m_SuctionRange = 5.0f;

    SwimController[] Players;

    private void Start()
    {
        Players = FindObjectsOfType<SwimController>();
        var shape = m_SuctionParticle.shape;
        shape.angle = m_SuctionAngle;
        shape.length = m_SuctionRange + 1;
    }

    private void Update()
    {
        Suck();

        Debug.DrawLine(transform.position, transform.position - transform.forward, Color.red);
    }

    void Suck()
    {
        float distanceToPlayer = 0.0f;
        float angleToPlayer = 0.0f;
        Vector3 playerPos;
        foreach(SwimController player in Players)
        {
            playerPos = player.transform.position;
            playerPos.z = transform.position.z;
            distanceToPlayer = Vector3.Distance(playerPos, transform.position);
            if (distanceToPlayer < m_SuctionRange)
            {
                angleToPlayer = Vector3.Angle(-transform.forward, playerPos - transform.position);
                Debug.Log(angleToPlayer);
                if (angleToPlayer < m_SuctionAngle)
                {
                    player.ApplyForce((transform.position - playerPos).normalized * m_SuctionStrength);
                }
            }
        }
    }
}
