using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int maxHealth = 2;
    [SerializeField] int currentHealth;
    [SerializeField] Material[] materials;
    [SerializeField] Color[] materialColors;

    [SerializeField] Color flashColor = Color.white;

    [SerializeField] float flashTime = 0.2f;
    private void Start()
    {
        currentHealth = maxHealth;
        Renderer[] renderer = GetComponentsInChildren<Renderer>();
        materials = new Material[renderer.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i] = renderer[i].material;
        }
        materialColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            materialColors[i] = materials[i].color;
        }
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
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = flashColor;
        }

        yield return new WaitForSeconds(flashTime);
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = materialColors[i];
        }
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
