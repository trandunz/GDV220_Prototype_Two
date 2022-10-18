using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMBuoyManager : MonoBehaviour
{
    [SerializeField] MMBuoy[] m_Buoys;
    int m_CurrentSelelection = 0;
    public GameObject audioSplash;
    [SerializeField] GameObject PauseMenu;
    bool m_inPauseMenu = false;

    private void Start()
    {
        PauseMenu.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!m_inPauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Destroy(Instantiate(audioSplash), 3.0f);
                m_CurrentSelelection++;
                if (m_CurrentSelelection >= m_Buoys.Length)
                {
                    m_CurrentSelelection = 0;
                }
                Debug.Log(m_CurrentSelelection);
            }
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Destroy(Instantiate(audioSplash), 3.0f);
                m_CurrentSelelection--;
                if (m_CurrentSelelection < 0)
                {
                    m_CurrentSelelection = m_Buoys.Length - 1;
                }
                Debug.Log(m_CurrentSelelection);
            }

            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Backspace))
            {   
                Debug.Log("Option Chosen = " + m_CurrentSelelection);

                if (m_CurrentSelelection == 0)
                    LevelLoader.instance.LoadLevel(1);
                else if (m_CurrentSelelection == 1)
                    PauseMenu.SetActive(true);
                else if (m_CurrentSelelection == 2)
                    LevelLoader.instance.LoadLevel(1);
                else if (m_CurrentSelelection == 3)
                    Application.Quit();
            }

            for (int i = 0; i < m_Buoys.Length; i++)
            {
                if (i != m_CurrentSelelection)
                    m_Buoys[i].SetSelected(false);
                else
                    m_Buoys[i].SetSelected(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenu.SetActive(false);
            }
        }

        m_inPauseMenu = PauseMenu.activeSelf;
    }
}
