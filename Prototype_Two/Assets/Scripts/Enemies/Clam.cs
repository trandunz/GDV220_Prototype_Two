using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clam : MonoBehaviour
{
    [SerializeField] ParticleSystem m_SuctionParticle;
    [SerializeField] ParticleSystem m_LinesParticle;

    [Header("Suction Settings")]
    [SerializeField] float m_SuctionStrength = 100.0f;
    [SerializeField] float m_SuctionAngle = 15.0f;
    [SerializeField] float m_SuctionRange = 5.0f;

    bool m_HasAttacked = false;

    Animator m_Animator;
    SwimController[] Players;

    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
        Players = FindObjectsOfType<SwimController>();
        var shape = m_SuctionParticle.shape;
        shape.angle = m_SuctionAngle;
        shape.length = m_SuctionRange;

        var LinesShape = m_LinesParticle.shape;
        LinesShape.angle = m_SuctionAngle;
        LinesShape.length = m_SuctionRange;
    }

    private void Update()
    {
        if (!m_HasAttacked)
            Suck();

        Debug.DrawLine(transform.position, transform.position - transform.forward, Color.red);
    }

    IEnumerator AttackRoutine()
    {
        m_HasAttacked = true;
        m_Animator.SetTrigger("Attack");
        m_SuctionParticle.gameObject.SetActive(false);
        m_LinesParticle.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        m_HasAttacked = false;
        m_SuctionParticle.gameObject.SetActive(true);
        m_LinesParticle.gameObject.SetActive(true);
        m_LinesParticle.Play();
        m_SuctionParticle.Play();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SwimController player = other.GetComponent<SwimController>();
            StopAllCoroutines();
            StartCoroutine(DamageRoutine(player));
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator DamageRoutine(SwimController player)
    {
        yield return new WaitForSeconds(0.5f);
        player.HitEnemy();
        player.ApplyImpulse(transform.forward * -300);
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
                if (distanceToPlayer < 3.0f)
                {
                    return;
                }
                else
                {
                    angleToPlayer = Vector3.Angle(-transform.forward, playerPos - transform.position);
                    if (angleToPlayer < m_SuctionAngle)
                    {
                        player.ApplyForce((transform.position - playerPos).normalized * m_SuctionStrength);
                    }
                }
            }
        }
    }
}
