using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Image BoathubIcon;
    [SerializeField] Image UpgradesIcon;
    [SerializeField] Image SettingsIcon;
    [SerializeField] Image HelpIcon;

    [SerializeField] TMPro.TextMeshProUGUI Title;
    [SerializeField] GameObject Boathub;
    [SerializeField] GameObject Upgrades;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Help;

    bool doOnce = true;

    private void Start()
    {
        BoathubIcon.color = Color.white;
        SwitchToBoathub();
        
    }

    public void SwitchToBoathub()
    {
        Title.text = "BOATHUB";
        HoverLeftOption(UpgradesIcon);
        HoverLeftOption(SettingsIcon);
        HoverLeftOption(HelpIcon);
        DisableAllMenus();
        Boathub.SetActive(true);

        HoverOverOption(BoathubIcon);
    }
    public void SwitchToUpgrades()
    {
        Title.text = "UPGRADES";
        HoverLeftOption(BoathubIcon);
        HoverLeftOption(SettingsIcon);
        HoverLeftOption(HelpIcon);
        DisableAllMenus();
        Upgrades.SetActive(true);

        HoverOverOption(UpgradesIcon);
    }
    public void SwitchToSettings()
    {
        Title.text = "SETTINGS";
        HoverLeftOption(BoathubIcon);
        HoverLeftOption(UpgradesIcon);
        HoverLeftOption(HelpIcon);
        DisableAllMenus();
        Settings.SetActive(true);

        HoverOverOption(SettingsIcon);
    }
    public void SwitchToHelp()
    {
        Title.text = "MANUAL";
        HoverLeftOption(BoathubIcon);
        HoverLeftOption(UpgradesIcon);
        HoverLeftOption(SettingsIcon);
        DisableAllMenus();
        Help.SetActive(true);

        HoverOverOption(HelpIcon);
    }

    public void DisableAllMenus()
    {
        Boathub.SetActive(false);
        Upgrades.SetActive(false);
        Settings.SetActive(false);
        Help.SetActive(false);
    }

    void HoverOverOption(Image _image)
    {
        Color color = _image.color;
        color.a = 1;
        StartCoroutine(LerpColor(_image, color, 0.15f));
    }

    void HoverLeftOption(Image _image)
    {
        Color color = _image.color;
        color.a = 0.5f;
        StartCoroutine(LerpColor(_image, color, 0.15f));
    }

    IEnumerator LerpColor(Image _image, Color _endColor, float _fadeTime)
    {
        float timeElapsed = 0f;
        while (timeElapsed < _fadeTime)
        {
            _image.color = Color.Lerp(_image.color, _endColor, timeElapsed / _fadeTime);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
