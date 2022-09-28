using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBuffChest : MonoBehaviour
{
    [SerializeField] GameObject[] m_BubbleBuffObjects;
    [SerializeField] GameObject m_BubbleBuffObject;
    [SerializeField] Transform m_BubbleBuffBubble;
    BubbleBuff m_BubbleBuffScript;
    bool IsUsed = false;

    private void Start()
    {
        m_BubbleBuffObject = Instantiate(m_BubbleBuffObjects[Random.Range(0, m_BubbleBuffObjects.Length)], m_BubbleBuffBubble);
        m_BubbleBuffObject.transform.position = m_BubbleBuffBubble.transform.position;
        m_BubbleBuffScript = m_BubbleBuffObject.GetComponent<BubbleBuff>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player" && !IsUsed)
        {
            m_BubbleBuffScript.GiveToPlayer();
            IsUsed = true;
        }
    }
}
