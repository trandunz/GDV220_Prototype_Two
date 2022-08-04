using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Button BoathubIcon;
    [SerializeField] Button UpgradesIcon;
    [SerializeField] Button SettingsIcon;
    [SerializeField] Button HelpIcon;

    [SerializeField] GameObject Title;
    [SerializeField] GameObject Boathub;
    [SerializeField] GameObject Upgrades;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Help;

    private void Start()
    {
        DisableAllMenus();
        SwitchToBoathub();
       
    }

    public void SwitchToBoathub()
    {
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "BOATHUB";
        DisableAllMenus();
        Boathub.SetActive(true);

        Color color = BoathubIcon.image.color;
        color.a = 1;
        BoathubIcon.image.color = color;
    }
    public void SwitchToUpgrades()
    {
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "UPGRADES";
        DisableAllMenus();
        Upgrades.SetActive(true);

        Color color = UpgradesIcon.image.color;
        color.a = 1;
        UpgradesIcon.image.color = color;
    }
    public void SwitchToSettings()
    {
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "SETTINGS";
        DisableAllMenus();
        Settings.SetActive(true);

        Color color = SettingsIcon.image.color;
        color.a = 1;
        SettingsIcon.image.color = color;
    }
    public void SwitchToHelp()
    {
        Title.GetComponent<TMPro.TextMeshProUGUI>().text = "MANUAL";
        DisableAllMenus();
        Help.SetActive(true);

        Color color = HelpIcon.image.color;
        color.a = 1;
        HelpIcon.image.color = color;
    }

    public void DisableAllMenus()
    {
        Color color = BoathubIcon.image.color;
        color.a = 0.5f;
        BoathubIcon.image.color = color;

        color = UpgradesIcon.image.color;
        color.a = 0.5f;
        UpgradesIcon.image.color = color;

        color = SettingsIcon.image.color;
        color.a = 0.5f;
        SettingsIcon.image.color = color;

        color = HelpIcon.image.color;
        color.a = 0.5f;
        HelpIcon.image.color = color;

        Boathub.SetActive(false);
        Upgrades.SetActive(false);
        Settings.SetActive(false);
        Help.SetActive(false);
    }
}
