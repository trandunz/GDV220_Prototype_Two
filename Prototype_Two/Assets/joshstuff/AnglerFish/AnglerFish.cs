using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglerFish : MonoBehaviour
{
    [SerializeField] GameObject gem;
    public float moveSpeed = 5;
    [SerializeField] float lifeTime = 5;
    [SerializeField] bool isMoving = false;

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
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
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<SwimController>().HitEnemy();
            Attack();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().StartInvulnrability();
            Attack();
        }
    }
}
