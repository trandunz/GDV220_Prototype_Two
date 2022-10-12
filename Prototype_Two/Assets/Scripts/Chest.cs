using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator m_Animator;
    public bool m_IsOpen = false;
    public bool m_Locked = false;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
    }
    public void OpenChest()
    {
        if (!m_IsOpen && !m_Locked)
        {
            m_IsOpen = true;
            m_Animator.SetBool("Open", true);
        }
    }

    public void CloseChest()
    {
        if (m_IsOpen && !m_Locked)
        {
            m_Locked = true;
            m_IsOpen = false;
            m_Animator.SetBool("Open", false);
        }
    }
}
