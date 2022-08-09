using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoathubPanel : MonoBehaviour
{
    [SerializeField] Button MainMenu;
    [SerializeField] Button start;
    [SerializeField] Button RecordFull;
    [SerializeField] Button RecordEmpty;

    [Header("Audio")]
    public GameObject MouseOver;
    public GameObject Select;

    TMPro.TextMeshProUGUI RecordText;
    private void Start()
    {
        RecordText = RecordEmpty.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Color textColor = RecordText.color;
        textColor.a = 0;
        RecordText.color = textColor;

        HoverLeftOption(MainMenu.image);
        HoverLeftOption(start.image);
        HoverLeftOption(RecordEmpty.image);
    }

    public void OnMainMenuClick()
    {
        LevelLoader.instance.LoadLevel(0);
        Destroy(Instantiate(Select), 1.0f);
    }

    public void PointerOverMainMenu()
    {
        HoverOverOption(MainMenu.image);
        Destroy(Instantiate(MouseOver), 1.0f);
    }

    public void PointerLeftMainMenu()
    {
        HoverLeftOption(MainMenu.image);
    }

    public void OnStartClick()
    {
        LevelLoader.instance.LoadLevel(2);
        Destroy(Instantiate(Select), 1.0f);
    }
    public void PointerOverStart()
    {
        HoverOverOption(start.image);
        Destroy(Instantiate(MouseOver), 1.0f);
    }

    public void PointerLeftStart()
    {
        HoverLeftOption(start.image);
    }

    public void OnRecordsClick()
    {
        Destroy(Instantiate(Select), 1.0f);
    }
    public void PointerOverRecords()
    {
        Destroy(Instantiate(MouseOver), 1.0f);

        Color textColor2 = RecordText.color;
        textColor2.a = 1;
        StartCoroutine(LerpColor(RecordText, textColor2, 0.15f));

        RecordText.text = PlayerPrefs.GetInt("DeepestDepth") + "m";

        HoverOverOption(RecordEmpty.image);
        StartCoroutine(LerpScale(RecordFull.transform, new Vector3(1.2f, 1.2f, 1), 0.15f));
        Color color = RecordFull.image.color;
        color.a = 0;
        StartCoroutine(LerpColor(RecordFull.image, color, 0.15f));
    }

    public void PointerLeftRecords()
    {

        Color textColor2 = RecordText.color;
        textColor2.a = 0;
        StartCoroutine(LerpColor(RecordText, textColor2, 0.15f));

        HoverLeftOption(RecordFull.image);
        StartCoroutine(LerpScale(RecordEmpty.transform, new Vector3(1.0f, 1.0f, 1), 0.15f));
        Color color = RecordEmpty.image.color;
        color.a = 0;
        StartCoroutine(LerpColor(RecordEmpty.image, color, 0.15f));
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
