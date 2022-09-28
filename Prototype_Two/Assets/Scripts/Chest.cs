using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator m_Animator;
    bool m_IsOpen = false;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        if (!m_IsOpen)
        {
            m_IsOpen = true;
            m_Animator.SetBool("Open", true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "Player")
        {
            OpenChest();
        }
    }
}
