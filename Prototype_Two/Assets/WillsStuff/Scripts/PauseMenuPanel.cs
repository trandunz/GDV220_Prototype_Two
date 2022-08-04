using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuPanel : MonoBehaviour
{
    [SerializeField] Button Boathub;
    [SerializeField] Button Resume;
    [SerializeField] Button Settings;

    public void GotoBoathouse()
    {
        SceneManager.LoadScene(1);
    }

    public void OnResume()
    { 
    }

    public void PointerOverBoathub()
    {
        GreyOutAllOptions();
        Color Boathubcolor = Boathub.image.color;
        Boathubcolor.a = 255;
        Boathub.image.color = Boathubcolor;
        Boathub.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void PointerLeftBoathub()
    {
        GreyOutAllOptions();
        Color Boathubcolor = Boathub.image.color;
        Boathubcolor.a = 255;
        Boathub.image.color = Boathubcolor;
        Boathub.transform.localScale = new Vector3(1, 1, 1);
    }
    public void PointerOverResume()
    {
        GreyOutAllOptions();
        Color Resumecolor = Resume.image.color;
        Resumecolor.a = 128;
        Resume.image.color = Resumecolor;
        Resume.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void PointerLeftResume()
    {
        GreyOutAllOptions();
        Color Boathubcolor = Boathub.image.color;
        Boathubcolor.a = 128;
        Boathub.image.color = Boathubcolor;
        Boathub.transform.localScale = new Vector3(1, 1, 1);
    }
    public void PointerOverSettingss()
    {
        GreyOutAllOptions();
        Color Settingscolor = Settings.image.color;
        Settingscolor.a = 255;
        Settings.image.color = Settingscolor;
        Settings.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void PointerLeftSettingss()
    {
        GreyOutAllOptions();
        Color Boathubcolor = Boathub.image.color;
        Boathubcolor.a = 128;
        Boathub.image.color = Boathubcolor;
        Boathub.transform.localScale = new Vector3(1, 1, 1);
    }
    void GreyOutAllOptions()
    {
        Color Resumecolor = Resume.image.color;
        Resumecolor.a = 128;
        Resume.image.color = Resumecolor;
        Resume.transform.localScale = new Vector3(1.0f, 1.0f, 1);

        Color Boathubcolor = Boathub.image.color;
        Boathubcolor.a = 128;
        Boathub.image.color = Boathubcolor;
        Boathub.transform.localScale = new Vector3(1.0f, 1.0f, 1);

        Color Settingscolor = Settings.image.color;
        Settingscolor.a = 128;
        Settings.image.color = Settingscolor;
        Settings.transform.localScale = new Vector3(1.0f, 1.0f, 1);
    }
}
