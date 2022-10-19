using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipBook : MonoBehaviour
{
    [SerializeField] Image[] Pages;
    [SerializeField] Button[] Buttons;
    [SerializeField] GameObject PauseMenu;
    int m_CurrentPage = 0;

    private void OnEnable()
    {
        m_CurrentPage = 0;
    }

    private void Update()
    {
        if (!PauseMenu.activeSelf)
        {
            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
                NextPage();
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
                PreviousPage();
            if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Backspace))
                gameObject.SetActive(false);

            if (m_CurrentPage <= 0)
                Buttons[0].gameObject.SetActive(false);
            else
                Buttons[0].gameObject.SetActive(true);

            if (m_CurrentPage >= Pages.Length - 1)
                Buttons[1].gameObject.SetActive(false);
            else
                Buttons[1].gameObject.SetActive(true);

            for (int i = 0; i < Pages.Length; i++)
            {
                if (i == m_CurrentPage)
                    Pages[i].gameObject.SetActive(true);
                else
                    Pages[i].gameObject.SetActive(false);
            }
        }
    }

    public void NextPage()
    {
        m_CurrentPage++;
        ClampToMaxPages();
    }    

    public void PreviousPage()
    {
        m_CurrentPage--;
        ClampToMaxPages();
    }
    
    void ClampToMaxPages()
    {
        m_CurrentPage = Mathf.Clamp(m_CurrentPage, 0, Pages.Length - 1);
    }
}
