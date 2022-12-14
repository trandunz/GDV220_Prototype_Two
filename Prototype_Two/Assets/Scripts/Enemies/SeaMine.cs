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

    [Header("Explosion Settings")]
    [SerializeField] float m_ScreenShakeAmount = 5.0f;
    [SerializeField] float m_ScreenShakeTime = 2.0f;
    [SerializeField] float m_ExplosionForce = 10.0f;
    [SerializeField] float m_ExplosionRadius = 2.0f;
    [SerializeField] float m_ExplodeTime = 0.3f;
    bool m_Activated = false;

    ScreenShake shake;
    Vector3 m_StartPos;
    Quaternion m_StartRotation;
    [SerializeField] GameObject[] objectsToKeep;
    [SerializeField] GameObject head;

    [SerializeField] GameObject ExplosionPart;

    [Header("Audio")]
    public GameObject audioSeaMine;

    private void Start()
    {
        shake = FindObjectOfType<ScreenShake>();
        transform.rotation = Quaternion.Euler((float)Random.Range(0, 2), (float)Random.Range(0, 2),2);
        m_StartRotation = transform.rotation;
        m_StartPos = transform.position;
    }
    private void Update()
    {
        transform.position = m_StartPos + new Vector3(Mathf.Cos(m_HorizontalBobSpeed * Time.time) * m_HorizontalBobAmp, Mathf.Sin(m_VerticalBobSpeed * Time.time) * m_VerticalBobAmp, 0.0f);
        transform.rotation = m_StartRotation * Quaternion.Euler(Mathf.Sin(Time.time * m_RockSpeed) * 2.0f, transform.rotation.y, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !m_Activated)
        {
            Destroy(Instantiate(audioSeaMine), 3.0f);
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

        shake.StartShake(m_ScreenShakeAmount, m_ScreenShakeTime);

        GameObject newOwner = Instantiate(head, transform.position, Quaternion.identity);
        Destroy(newOwner, 60);
        for (int i = 0; i < objectsToKeep.Length; i++)
        {
            objectsToKeep[i].transform.SetParent(newOwner.transform);
        }
        Destroy(gameObject);
        Destroy(Instantiate(ExplosionPart, transform.position, Quaternion.identity), 5.0f);
    }
}
