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

    [SerializeField] GameObject TopPanel;
    [SerializeField] GameObject BotPanel;
    [SerializeField] GameObject MainPanel;
    [SerializeField] GameObject OptionsWrapper;

    bool IsOpen = false;
    bool IsTransitioning = false;

    private void Start()
    {
        HoverLeftOption(Boathub.image);
        HoverLeftOption(Resume.image);
        HoverLeftOption(Settings.image);
        UpdateChildActive();
    }

    private void Update()
    {
        if (Input.GetKeyDown((KeyCode)PlayerPrefs.GetInt("PauseBind")) && !IsOpen)
        {
            TogglePauseMenu();
        }
    }

    void TogglePauseMenu()
    {
        IsOpen = !IsOpen;
        if (IsOpen)
        {
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
        UpdateChildActive();
    }

    void UpdateChildActive()
    {
        if (IsOpen)
        {
            TopPanel.SetActive(true);
            BotPanel.SetActive(true);
            MainPanel.SetActive(true);
            OptionsWrapper.SetActive(false);
            Color panelColor = GetComponent<Image>().color;
            panelColor.a = 0.5f;
            GetComponent<Image>().color = panelColor;
        }
        else
        {
            ResetAllButtons();
            TopPanel.SetActive(false);
            BotPanel.SetActive(false);
            Color panelColor = GetComponent<Image>().color;
            panelColor.a = 0;
            GetComponent<Image>().color = panelColor;
            MainPanel.SetActive(false);
            OptionsWrapper.SetActive(false);
        }
    }

    public void GotoBoathouse()
    {
        Time.timeScale = 1.0f;
        LevelLoader.instance.LoadLevel(1);
    }

    public void OnResume()
    {
        Time.timeScale = 1.0f;
        IsOpen = false;
        UpdateChildActive();
    }

    public void PointerOverBoathub()
    {
        HoverOverOption(Boathub.image);
    }

    public void PointerLeftBoathub()
    {
        HoverLeftOption(Boathub.image);
    }
    public void PointerOverResume()
    {
        HoverOverOption(Resume.image);
    }

    public void PointerLeftResume()
    {
        HoverLeftOption(Resume.image);
    }
    public void PointerOverSettings()
    {
        HoverOverOption(Settings.image);
    }

    public void PointerLeftSettings()
    {
        HoverLeftOption(Settings.image);
    }

    void HoverOverOption(Image _image)
    {
        Color color = _image.color;
        color.a = 1;
        StartCoroutine(LerpColor(_image, color, 0.15f));
        StartCoroutine(LerpScale(_image.transform, new Vector3(1.2f, 1.2f, 1), 0.15f));
    }

    void HoverLeftOption(Image _image)
    {
        Color color = _image.color;
        color.a = 0.5f;
        StartCoroutine(LerpColor(_image, color, 0.15f));
        StartCoroutine(LerpScale(_image.transform, new Vector3(1.0f, 1.0f, 1), 0.15f));
    }

    void ResetAllButtons()
    {
        ForceResetButton(Boathub.image);
        ForceResetButton(Resume.image);
        ForceResetButton(Settings.image);
    }

    public void ForceResetButton(Image _image)
    {
        Color color = _image.color;
        color.a = 0.5f;
        _image.color = color;
        _image.transform.localScale = Vector3.one;
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
    IEnumerator LerpScale(Transform _transform, Vector3 _endScale, float _transitionTime)
    {
        float timeElapsed = 0f;
        while (timeElapsed < _transitionTime)
        {
            _transform.localScale = Vector3.Lerp(_transform.localScale, _endScale, timeElapsed / _transitionTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}