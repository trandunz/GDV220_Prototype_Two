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

    private void Start()
    {
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
        FindObjectOfType<LevelLoader>().LoadLevel(2);
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
    }

    public void PointerLeftRecords()
    {
        HoverLeftOption(Record.image);
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
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    IEnumerator LerpScale(Transform _transform, Vector2 _endScale, float _transitionTime)
    {
        float timeElapsed = 0f;
        while(timeElapsed < _transitionTime)
        {
            _transform.localScale = Vector3.Lerp(_transform.localScale, _endScale, timeElapsed / _transitionTime);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
