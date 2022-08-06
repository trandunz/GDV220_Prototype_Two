using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoathubPanel : MonoBehaviour
{
    [SerializeField] Button MainMenu;
    [SerializeField] Button start;
    [SerializeField] Button Record;

    TMPro.TextMeshProUGUI[] RecordTexts;
    private void Start()
    {
        RecordTexts = Record.GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        Color textColor = RecordTexts[1].color;
        textColor.a = 0;
        RecordTexts[1].color = textColor;

        HoverLeftOption(MainMenu.image);
        HoverLeftOption(start.image);
        HoverLeftOption(Record.image);
    }

    public void OnMainMenuClick()
    {
        LevelLoader.instance.LoadLevel(0);
    }

    public void PointerOverMainMenu()
    {
        HoverOverOption(MainMenu.image);
    }

    public void PointerLeftMainMenu()
    {
        HoverLeftOption(MainMenu.image);
    }

    public void OnStartClick()
    {
        LevelLoader.instance.LoadLevel(2);
    }
    public void PointerOverStart()
    {
        HoverOverOption(start.image);
    }

    public void PointerLeftStart()
    {
        HoverLeftOption(start.image);
    }

    public void OnRecordsClick()
    {
        
    }
    public void PointerOverRecords()
    {
        HoverOverOption(Record.image);
        Color textColor = RecordTexts[0].color;
        textColor.a = 0;
        StartCoroutine(LerpColor(RecordTexts[0], textColor, 0.15f));

        Color textColor2 = RecordTexts[1].color;
        textColor2.a = 1;
        StartCoroutine(LerpColor(RecordTexts[1], textColor2, 0.15f));

        RecordTexts[1].text = "Deepest\nExpedition\n" + PlayerPrefs.GetInt("DeepestDepth") + "m";
    }

    public void PointerLeftRecords()
    {
        HoverLeftOption(Record.image);
        Color textColor = RecordTexts[0].color;
        textColor.a = 1;
        StartCoroutine(LerpColor(RecordTexts[0], textColor, 0.15f));

        Color textColor2 = RecordTexts[1].color;
        textColor2.a = 0;
        StartCoroutine(LerpColor(RecordTexts[1], textColor2, 0.15f));
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

    IEnumerator LerpScale(Transform _transform, Vector2 _endScale, float _transitionTime)
    {
        float timeElapsed = 0f;
        while(timeElapsed < _transitionTime)
        {
            _transform.localScale = Vector3.Lerp(_transform.localScale, _endScale, timeElapsed / _transitionTime);
            timeElapsed += Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
