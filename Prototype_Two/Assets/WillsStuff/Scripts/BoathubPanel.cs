using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoathubPanel : MonoBehaviour
{
    [SerializeField] Button start;

    [Header("Audio")]
    public GameObject MouseOver;
    public GameObject Select;

    bool HoverStart = true;

    private void Start()
    {
    }
    private void Update()
    {
        if (HoverStart)
        {
            HoverOverOption(start.image);
            if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Q))
            {
                OnStartClick();
            }
        }
        else
            HoverLeftOption(start.image);
    }


    public void OnMainMenuClick()
    {
        LevelLoader.instance.LoadLevel(0);
        Destroy(Instantiate(Select), 1.0f);
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
