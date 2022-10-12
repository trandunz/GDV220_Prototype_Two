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
            Vector3 impulse = new Vector3(knockBackForce, 0.0f, 0.0f);
            if (transform.rotation.eulerAngles.y == 180.0f) // Facing right
            {
                impulse = new Vector3(-knockBackForce, 0.0f, 0.0f);
            }
            Debug.Log(impulse.x);
            other.GetComponent<SwimController>().ApplyImpulse(impulse);
            other.GetComponent<SwimController>().HitEnemy();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player")
        {
            other.GetComponent<SwimController>().StartInvulnrability();
        }
    }
}
