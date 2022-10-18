using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMBuoyManager : MonoBehaviour
{
    MMBuoy[] m_Buoys;
    int m_CurrentSelelection = 0;
    public GameObject audioSplash;

    private void Start()
    {
        m_Buoys = FindObjectsOfType<MMBuoy>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Destroy(Instantiate(audioSplash), 3.0f);
            m_CurrentSelelection++;
            if (m_CurrentSelelection >= m_Buoys.Length)
            {
                m_CurrentSelelection = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Destroy(Instantiate(audioSplash), 3.0f);
            m_CurrentSelelection--;
            if (m_CurrentSelelection < 0)
            {
                m_CurrentSelelection = m_Buoys.Length - 1;
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Backspace))
        {
            if (m_CurrentSelelection == 0)
                LevelLoader.instance.LoadLevel(1);
            else if (m_CurrentSelelection == 1)
                LevelLoader.instance.LoadLevel(1);
            else if (m_CurrentSelelection == 2)
                Application.Quit();
        }

        for(int i = 0; i < m_Buoys.Length; i++)
        {
            if (i != m_CurrentSelelection)
                m_Buoys[i].SetSelected(false);
            else
                m_Buoys[i].SetSelected(true);
        }


    }
}
