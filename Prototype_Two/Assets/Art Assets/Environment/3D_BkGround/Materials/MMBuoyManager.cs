using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMBuoyManager : MonoBehaviour
{
    [SerializeField] MMBuoy[] m_Buoys;
    int m_CurrentSelelection = 0;
    public GameObject audioSplash;
    [SerializeField] GameObject PauseMenu;
    [SerializeField] GameObject FlipBook;
    [SerializeField] GameObject Highscores;
    bool m_inPauseMenu = false;
    bool m_inHighScore = false;
    bool m_inFlipBook = false;
    bool m_inHighScores = false;
    bool canDoStuff = false;
    [SerializeField] float m_StartDelay = 0.5f;

    private void Start()
    {
        PauseMenu.gameObject.SetActive(false);
        FlipBook.SetActive(false);
        Highscores.SetActive(false);
    }
    private void Update()
    {
        if (m_StartDelay > 0)
        {
            m_StartDelay -= Time.deltaTime;
        }
        else
        {
            if (!canDoStuff)
            {
                Destroy(Instantiate(audioSplash), 3.0f);
                canDoStuff = true;
            }
            
            if (!m_inPauseMenu && !m_inHighScore && !m_inFlipBook && !m_inHighScores)
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
                        Highscores.SetActive(true);
                    else if (m_CurrentSelelection == 3)
                        FlipBook.SetActive(true);
                    else if (m_CurrentSelelection == 4)
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
        }
        

        m_inHighScore = FindObjectOfType<EnterHighScore>();
        m_inFlipBook = FlipBook.activeSelf;
        m_inPauseMenu = PauseMenu.activeSelf;
        m_inHighScores = Highscores.activeSelf;
    }
}
