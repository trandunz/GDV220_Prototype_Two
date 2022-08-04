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

    private void Start()
    {
        HoverLeftOption(Boathub.image);
        HoverLeftOption(Resume.image);
        HoverLeftOption(Settings.image);
    }

    public void GotoBoathouse()
    {
        LevelLoader.instance.LoadLevel(1);
    }

    public void OnResume()
    {
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
        while (timeElapsed < _transitionTime)
        {
            _transform.localScale = Vector3.Lerp(_transform.localScale, _endScale, timeElapsed / _transitionTime);
            timeElapsed += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}