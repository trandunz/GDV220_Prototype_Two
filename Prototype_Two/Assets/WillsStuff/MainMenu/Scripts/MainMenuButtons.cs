using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject audioSelect;
    Color originalColor;

    public void StartGame()
    {
        Destroy(Instantiate(audioSelect), 2.0f);
        LevelLoader.instance.LoadLevel(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu(GameObject _menu)
    {
        _menu.SetActive(!_menu.activeSelf);
    }

    public void HoverOverOption(TMPro.TextMeshProUGUI _text)
    {
        originalColor = _text.color;
        Color color = new Color(255, 255, 255);
        StartCoroutine(LerpColor(_text, color, 0.15f));
    }

    public void HoverOverOption(Image _text)
    {
        originalColor = _text.color;
        Color color = new Color(255, 255, 255);
        StartCoroutine(LerpColor(_text, color, 0.15f));
    }

    public void HoverLeftOption(TMPro.TextMeshProUGUI _text)
    {
        Debug.Log("Left Option");
        Color color = originalColor;
        StartCoroutine(LerpColor(_text, color, 0.15f));
    }

    public void HoverLeftOption(Image _text)
    {
        Debug.Log("Left Option");
        Color color = originalColor;
        StartCoroutine(LerpColor(_text, color, 0.15f));
    }

    IEnumerator LerpColor(TMPro.TextMeshProUGUI _text, Color _endColor, float _fadeTime)
    {
        float timeElapsed = 0f;
        while (timeElapsed < _fadeTime)
        {
            _text.color = Color.Lerp(_text.color, _endColor, timeElapsed / _fadeTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator LerpColor(Image _text, Color _endColor, float _fadeTime)
    {
        float timeElapsed = 0f;
        while (timeElapsed < _fadeTime)
        {
            _text.color = Color.Lerp(_text.color, _endColor, timeElapsed / _fadeTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
