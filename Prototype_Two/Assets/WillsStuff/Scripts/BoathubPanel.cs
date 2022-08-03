using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoathubPanel : MonoBehaviour
{
    [SerializeField] Button MainMenu;
    [SerializeField] Button Start;
    [SerializeField] Button Record;

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene(0);
    }

    public void PointerOverMainMenu()
    {
        GreyOutAllOptions();
        Color MainMenucolor = MainMenu.image.color;
        MainMenucolor.a = 255;
        MainMenu.image.color = MainMenucolor;
        MainMenu.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void PointerLeftMainMenu()
    {
        GreyOutAllOptions();
        Color MainMenucolor = MainMenu.image.color;
        MainMenucolor.a = 255;
        MainMenu.image.color = MainMenucolor;
        MainMenu.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnStartClick()
    {
        SceneManager.LoadScene(2);
    }
    public void PointerOverStart()
    {
        GreyOutAllOptions();
        Color Startcolor = Start.image.color;
        Startcolor.a = 128;
        Start.image.color = Startcolor;
        Start.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void PointerLeftStart()
    {
        GreyOutAllOptions();
        Color MainMenucolor = MainMenu.image.color;
        MainMenucolor.a = 128;
        MainMenu.image.color = MainMenucolor;
        MainMenu.transform.localScale = new Vector3(1, 1, 1);
    }

    public void OnRecordsClick()
    {
        
    }
    public void PointerOverRecords()
    {
        GreyOutAllOptions();
        Color Recordcolor = Record.image.color;
        Recordcolor.a = 255;
        Record.image.color = Recordcolor;
        Record.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void PointerLeftRecords()
    {
        GreyOutAllOptions();
        Color MainMenucolor = MainMenu.image.color;
        MainMenucolor.a = 128;
        MainMenu.image.color = MainMenucolor;
        MainMenu.transform.localScale = new Vector3(1, 1, 1);
    }

    public void GreyOutAllOptions()
    {
        Color Startcolor = Start.image.color;
        Startcolor.a = 128;
        Start.image.color = Startcolor;
        Start.transform.localScale = new Vector3(1.0f, 1.0f, 1);

        Color MainMenucolor = MainMenu.image.color;
        MainMenucolor.a = 128;
        MainMenu.image.color = MainMenucolor;
        MainMenu.transform.localScale = new Vector3(1.0f, 1.0f, 1);

        Color Recordcolor = Record.image.color;
        Recordcolor.a = 128;
        Record.image.color = Recordcolor;
        Record.transform.localScale = new Vector3(1.0f, 1.0f, 1);
    }
}
