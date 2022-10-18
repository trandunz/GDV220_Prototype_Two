using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squidy : MonoBehaviour
{
    [SerializeField] ParticleSystem m_InkParticles;
    [SerializeField] Material m_DefaultMat;
    [SerializeField] Material m_SuprisedMat;
    [SerializeField] SkinnedMeshRenderer m_Mesh;
    [Header("SwimSettings")]
    [SerializeField] float m_SwimDelay = 1.0f;
    [SerializeField] float m_SwimPower = 100.0f;
    [SerializeField] float m_SprintDelay = 0.5f;
    [SerializeField] float m_SprintPower = 200.0f;
    [SerializeField] float m_AlertRadius = 5.0f;
    [SerializeField] float m_TurnSpeed = 10.0f;
    float m_SwimTimer = 0.0f;
    Vector3 m_ToPlayerVector;
    Rigidbody rigidBody;
    SwimController[] Players;
    bool PlayerInRange = false;
    SpawnManager spawnManager;
    Animator animator;
    
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Players = FindObjectsOfType<SwimController>();
        animator = GetComponentInChildren<Animator>();
        m_Mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        m_Mesh.material = m_DefaultMat;

        m_ToPlayerVector = (GetClosestPlayer().transform.position - transform.position).normalized;
    }
    private void Update()
    {
        if (m_SwimTimer > 0)
            m_SwimTimer -= Time.deltaTime;

        if (!PlayerInRange)
        {
            Swim();

            if (Vector3.Distance(GetClosestPlayer().transform.position, transform.position) <= m_AlertRadius)
            {
                m_SwimTimer = 0.0f;
                rigidBody.velocity = Vector3.zero;
                PlayerInRange = true;
                m_Mesh.material = m_SuprisedMat;
                m_ToPlayerVector = (transform.position - GetClosestPlayer().transform.position).normalized;
            } 
        }
        if (PlayerInRange)
        {
            DashAtPlayer();
        }

        RotateTowardsVelocity();
    }
    void Swim()
    {
        if (m_SwimTimer <= 0)
        {
            animator.speed = 1.0f;

            m_SwimTimer = m_SwimDelay;

            rigidBody.AddForce(m_ToPlayerVector * m_SwimPower * Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    void DashAtPlayer()
    {
        if (m_SwimTimer <= 0)
        {
            animator.speed = 2.5f;

            m_InkParticles.Play();

            m_SwimTimer = m_SprintDelay;

            rigidBody.AddForce(m_ToPlayerVector * m_SprintPower* Time.fixedDeltaTime, ForceMode.Impulse);
        }
    }

    void RotateTowardsVelocity()
    {
        var dir = rigidBody.velocity;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = q;
    }

    SwimController GetClosestPlayer()
    {
        SwimController closestPlayer = Players[0];
        if (Vector3.Distance(Players[1].transform.position, transform.position) < Vector3.Distance(closestPlayer.transform.position, transform.position))
            closestPlayer = Players[1];

        return closestPlayer;
    }
}
