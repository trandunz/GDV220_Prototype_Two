using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBuffChest : MonoBehaviour
{
    [SerializeField] GameObject[] m_BubbleBuffObjects;
    [SerializeField] GameObject m_BubbleBuffObject;
    [SerializeField] Transform m_BubbleBuffBubble;
    [SerializeField] float m_DistanceToOpen = 4.0f;
    [SerializeField] float m_FloatHight = 1.35f;
    [SerializeField] float m_FloatUpSpeed = 3.0f;
    [SerializeField] float m_BobSpeed = 0.9f;
    [SerializeField] float m_BoBAmplitude = 0.3f;
    BubbleBuff m_BubbleBuffScript;
    Chest m_Chest;
    SwimController[] m_Players;
    float DistanceToClosestPlayer = float.MaxValue;
    bool IsUsed = false;

    private void Start()
    {
        m_FloatHight = m_BubbleBuffBubble.position.y + m_FloatHight;       
        m_Players = FindObjectsOfType<SwimController>();
        m_Chest = GetComponentInChildren<Chest>();
        m_BubbleBuffObject = Instantiate(m_BubbleBuffObjects[Random.Range(0, m_BubbleBuffObjects.Length)], m_BubbleBuffBubble);
        m_BubbleBuffObject.transform.position = m_BubbleBuffBubble.transform.position;
        m_BubbleBuffObject.transform.rotation = Quaternion.Euler(m_BubbleBuffObject.transform.rotation.eulerAngles.x, m_BubbleBuffObject.transform.eulerAngles.y + 180, m_BubbleBuffObject.transform.eulerAngles.z);
        m_BubbleBuffScript = m_BubbleBuffObject.GetComponentInChildren<BubbleBuff>();
    }

    private void Update()
    {
        GetDistanceToClosestPlayer();
        if (!m_Chest.m_IsOpen && DistanceToClosestPlayer < m_DistanceToOpen)
        {
            m_Chest.OpenChest();
            StartCoroutine(BubbleBuffFloatRoutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player" && !IsUsed)
        {
            SwimController player = other.GetComponent<SwimController>();
            if (!player.IsUsingBubbleBuff)
            {
                m_BubbleBuffScript.GiveToPlayer(player);
                m_Chest.CloseChest();
                Destroy(m_BubbleBuffBubble.gameObject);
                IsUsed = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag is "Player" && !IsUsed)
        {
            SwimController player = other.GetComponent<SwimController>();
            if (!player.IsUsingBubbleBuff)
            {
                m_BubbleBuffScript.GiveToPlayer(player);
                m_Chest.CloseChest();
                Destroy(m_BubbleBuffBubble.gameObject);
                IsUsed = true;
            }
        }
    }

    IEnumerator BubbleBuffFloatRoutine()
    {
        if (m_BubbleBuffBubble)
        {
            while (m_BubbleBuffBubble.position.y < m_FloatHight && m_Chest.m_IsOpen)
            {
                m_BubbleBuffBubble.position = m_BubbleBuffBubble.position + Vector3.up * m_FloatUpSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Vector3 startPos = m_BubbleBuffBubble.position;
            float elapsedTime = 0.0f;
            while (m_Chest.m_IsOpen)
            {
                m_BubbleBuffBubble.position = startPos + Vector3.up * Mathf.Sin(elapsedTime * m_BobSpeed) * m_BoBAmplitude;
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        else
            yield return null;
    }

    void GetDistanceToClosestPlayer()
    {
        float distanceToClosest = float.MaxValue;
        foreach(var player in m_Players)
        {
            Vector3 playerPos = player.transform.position;
            playerPos.z = 0.0f;
            Vector3 myPos = transform.position;
            myPos.z = 0.0f;
            float distance = Vector3.Distance(playerPos, myPos);
            if (distance < distanceToClosest)
            {
                distanceToClosest = distance;
            }
        }
        DistanceToClosestPlayer = distanceToClosest;
    }
}
