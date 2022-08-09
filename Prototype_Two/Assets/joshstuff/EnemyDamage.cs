using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int maxHealth = 2;
    [SerializeField] int currentHealth;

    [SerializeField] Flash flash;
    private void Start()
    {
        currentHealth = maxHealth;
        flash = GetComponent<Flash>();
    }

    void Die()
    {
        Destroy(gameObject);
        // make bubbles
    }

    void TakeDamage()
    {
        currentHealth -= 1;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        flash.FlashStart();

        yield return new WaitForSeconds(flash.flashTime);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}
