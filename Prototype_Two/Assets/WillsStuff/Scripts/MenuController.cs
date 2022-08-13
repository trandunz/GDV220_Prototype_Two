using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Image BoathubIcon;
    [SerializeField] Image SettingsIcon;
    [SerializeField] TMPro.TextMeshProUGUI Title;
    [SerializeField] GameObject Boathub;
    [SerializeField] GameObject Settings;

    [SerializeField] TMPro.TextMeshProUGUI OxygemCountText;

    public GameObject audioSelect;

    bool OnBoathub = true;
    bool OnSettings = false;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        BoathubIcon.color = Color.white;
        SwitchToBoathub();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && OnSettings)
        {
            SwitchToBoathub();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && OnBoathub)
        {
            SwitchToSettings();
        }

        OxygemCountText.text = "Highscore : " + PlayerPrefs.GetInt("DeepestDepth").ToString();
    }

    public void SwitchToBoathub()
    {
        OnBoathub = true;
        OnSettings = false;
        Title.text = "BOAT HUB";
        HoverLeftOption(SettingsIcon);
        DisableAllMenus();
        Boathub.SetActive(true);

        HoverOverOption(BoathubIcon);
        Destroy(Instantiate(audioSelect), 2.0f);
    }
    public void SwitchToSettings()
    {
        OnBoathub = false;
        OnSettings = true;

        Title.text = "SETTINGS";
        HoverLeftOption(BoathubIcon);
        DisableAllMenus();
        Settings.SetActive(true);

        HoverOverOption(SettingsIcon);
        Destroy(Instantiate(audioSelect), 2.0f);
    }

    public void DisableAllMenus()
    {
        Boathub.SetActive(false);
        Settings.SetActive(false);
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
