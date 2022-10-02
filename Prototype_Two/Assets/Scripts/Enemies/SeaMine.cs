using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMine : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float m_RockSpeed = 1.0f;
    [SerializeField] float m_VerticalBobSpeed = 1.0f;
    [SerializeField] float m_VerticalBobAmp = 1.0f;
    [SerializeField] float m_HorizontalBobSpeed = 5.0f;
    [SerializeField] float m_HorizontalBobAmp = 1.0f;

    [Header ("Explosion Settings")]
    [SerializeField] float m_ExplosionForce = 10.0f;
    [SerializeField] float m_ExplosionRadius = 2.0f;
    [SerializeField] float m_ExplodeTime = 0.3f;
    bool m_Activated = false;

    Vector3 m_StartPos;
    Quaternion m_StartRotation;

    private void Start()
    {
        transform.rotation = Quaternion.Euler((float)Random.Range(0, 90), (float)Random.Range(0, 90), (float)Random.Range(0, 90));
        m_StartRotation = transform.rotation;
        m_StartPos = transform.position;
    }
    private void Update()
    {
        transform.position = m_StartPos + new Vector3(Mathf.Cos(m_HorizontalBobSpeed * Time.time) * m_HorizontalBobAmp, Mathf.Sin(m_VerticalBobSpeed * Time.time) * m_VerticalBobAmp, 0.0f);
        transform.rotation = m_StartRotation * Quaternion.Euler(Mathf.Sin(Time.time * m_RockSpeed) * 2.0f, transform.rotation.y, Mathf.Cos(Time.time * m_RockSpeed) * 5.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !m_Activated)
        {
            StartCoroutine(ExplodeRoutine());
        }
    }

    IEnumerator ExplodeRoutine()
    {
        m_Activated = true;
        yield return new WaitForSeconds(m_ExplodeTime);

        foreach (SwimController player in FindObjectsOfType<SwimController>())
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= m_ExplosionRadius)
            {
                float normalizedDistance = Mathf.Lerp(1, 0, Vector3.Distance(player.transform.position, transform.position) / m_ExplosionRadius);
                Vector3 explosiveImpulse = (player.transform.position - transform.position).normalized;
                explosiveImpulse *= normalizedDistance * m_ExplosionForce;
                player.ApplyImpulse(explosiveImpulse);
                player.HitEnemy();
            }
        }

        Destroy(gameObject);
    }
}
