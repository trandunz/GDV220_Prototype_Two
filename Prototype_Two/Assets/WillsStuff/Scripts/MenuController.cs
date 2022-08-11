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
    public bool IsUpgradeAvailable = false;
    [SerializeField] Image UpgradeNotification;

    [SerializeField] TMPro.TextMeshProUGUI Title;
    [SerializeField] GameObject Boathub;
    [SerializeField] GameObject Upgrades;
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Help;

    [SerializeField] TMPro.TextMeshProUGUI OxygemCountText;

    public GameObject audioSelect;

    List<int> Prices;
    List<int> Levels;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        UpdateLevelsAndPrices();
        BoathubIcon.color = Color.white;
        SwitchToBoathub();
    }

    public void UpdateLevelsAndPrices()
    {
        Prices = GetComponentInChildren<UpgradesPanel>().GetAllPrices();
        Levels = GetComponentInChildren<UpgradesPanel>().GetAllLevels();
    }

    public void Update()
    {
        OxygemCountText.text = "X " + GemManager.instance.GetGemCount().ToString();

        IsUpgradeAvailable = true;
        foreach (var price in Prices)
        {
            if (GemManager.instance.GetGemCount() < price)
            {
                IsUpgradeAvailable = false;
            }
        }
        foreach(var level in Levels)
        {
            if (level >= 5)
            {
                IsUpgradeAvailable = false;
            }
        }

        if (IsUpgradeAvailable)
        {
            if (UpgradeNotification.color.a <= 0.0f)
            {
               /* Color startColor = UpgradeNotification.color;
                startColor.a = 0.1f;
                UpgradeNotification.color = startColor;*/

                Color newColor = UpgradeNotification.color;
                newColor.a = 1.0f;
                StartCoroutine(LerpColor(UpgradeNotification, newColor, 1));
            }
            else if (UpgradeNotification.color.a >= 1.0f)
            {
                /*Color startColor = UpgradeNotification.color;
                startColor.a = 0.9f;
                UpgradeNotification.color = startColor;*/

                Color newColor = UpgradeNotification.color;
                newColor.a = 0.0f;
                StartCoroutine(LerpColor(UpgradeNotification, newColor, 1));
            }
        }
        else
        {
            Color newColor = UpgradeNotification.color;
            newColor.a = 0.0f;
            UpgradeNotification.color = newColor;
        }
    }

    public void SwitchToBoathub()
    {
        Title.text = "BOAT HUB";
        HoverLeftOption(UpgradesIcon);
        HoverLeftOption(SettingsIcon);
        HoverLeftOption(HelpIcon);
        DisableAllMenus();
        Boathub.SetActive(true);

        HoverOverOption(BoathubIcon);
        Destroy(Instantiate(audioSelect), 2.0f);
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
        Destroy(Instantiate(audioSelect), 2.0f);
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
        Destroy(Instantiate(audioSelect), 2.0f);
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
        Destroy(Instantiate(audioSelect), 2.0f);
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
            timeElapsed += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
