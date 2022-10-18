using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject Settings;

    public GameObject audioSelect;

    bool OnSettings = false;

    private void OnEnable()
    {
        
    }
    private void Start()
    {
        SwitchToBoathub();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    public void SwitchToBoathub()
    {
        OnSettings = false;
        DisableAllMenus();
        Destroy(Instantiate(audioSelect), 2.0f);
    }
    public void SwitchToSettings()
    {
        OnSettings = true;

        DisableAllMenus();
        Settings.SetActive(true);

        Destroy(Instantiate(audioSelect), 2.0f);
    }

    public void DisableAllMenus()
    {
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
