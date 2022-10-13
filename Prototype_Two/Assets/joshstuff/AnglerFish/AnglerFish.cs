using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFish : MonoBehaviour
{
    [SerializeField] GameObject gem;
    public float moveSpeed = 5;
    [SerializeField] float lifeTime = 5;
    [SerializeField] bool isMoving = false;
    private float knockBackForce = 500.0f;
    [SerializeField] Animator m_Animator;
    bool hasAttacked = false;
    [SerializeField] Material m_RedAyeMaterial;
    [SerializeField] SkinnedMeshRenderer m_Eye;
    Material[] m_Materials;

    private void Start()
    {
        m_Materials = m_Eye.materials;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            m_Materials[1] = m_RedAyeMaterial;
            m_Eye.materials = m_Materials;

            transform.parent.position = Vector3.MoveTowards(transform.parent.position, gem.transform.position, moveSpeed * Time.deltaTime);
            lifeTime -= Time.deltaTime;

            if (lifeTime < 0)
            {
                Destroy(transform.root.gameObject);
            }
        }
    }

    public void Attack()
    {
        if (!hasAttacked)
        {
            hasAttacked = true;
            StartCoroutine(ChompRoutine());
        }
        
    }

    IEnumerator ChompRoutine()
    {
        m_Animator.SetTrigger("Attack");
        isMoving = true;
        float originalMoveSpeed = moveSpeed;
        moveSpeed = 3.0f;
        yield return new WaitForSeconds(0.4f);
        moveSpeed = originalMoveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 impulse = new Vector3(knockBackForce, 0.0f, 0.0f);
            if (transform.rotation.eulerAngles.y == 180.0f) // Facing right
            {
                impulse = new Vector3(-knockBackForce, 0.0f, 0.0f);
            }
            Debug.Log(impulse.x);
            other.GetComponent<SwimController>().ApplyImpulse(impulse);
            other.GetComponent<SwimController>().HitEnemy();
            isMoving = true;
            m_Animator.SetBool("Swim", true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().StartInvulnrability();
            isMoving = true;
        }
    }
}
