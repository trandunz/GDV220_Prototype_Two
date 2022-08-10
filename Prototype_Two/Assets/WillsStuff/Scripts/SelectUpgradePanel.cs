using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUpgradePanel : MonoBehaviour
{
    [SerializeField] UpgradeDescription description;

    public bool HasFinishedInit;
    int Level = 0;
    [SerializeField] Sprite EmptyTick;
    [SerializeField] Sprite FullTick;
    [SerializeField] Image[] IconsAndTicks;

    private void Start()
    {
        description.SetUpgradePanel(this);
        UpdateLevel();
        HasFinishedInit = true;
    }

    private void OnEnable()
    {
        GrabSavedLevel();
    }

    public void Upgrade()
    {
        if (Level < 5)
        {
            Level++;
            UpdateLevel();
            SaveLevel();
        }
    }

    void GrabSavedLevel()
    {
        Level = PlayerPrefs.GetInt(gameObject.name + " Level");
    }

    public void SaveLevel()
    {
        PlayerPrefs.SetInt(gameObject.name + " Level", Level);
    }

    public int GetLevel()
    {
        return Level;
    }

    void UpdateLevel()
    {
        SetGray();
        for (int i= Level + 1; i > 1; i--)
        {
            IconsAndTicks[i].sprite = FullTick;
            Color color = IconsAndTicks[i].color;
            color.r = 1.0f;
            color.g = 1.0f;
            color.b = 1.0f;
            if (HasFinishedInit)
            {
                if (i == Level + 1)
                    StartCoroutine(LerpColor(IconsAndTicks[i], color, 0.15f));
                else
                    IconsAndTicks[i].color = color;
            }
            else
                IconsAndTicks[i].color = color;
        }
    }

    void SetGray()
    {
        for (int i = 2; i <= 6; i++)
        {
            IconsAndTicks[i].sprite = EmptyTick;
            Color color = IconsAndTicks[i].color;
            color.r = 0.66f;
            color.g = 0.66f;
            color.b = 0.66f;
            if (HasFinishedInit)
                if (i == Level + 1)
                    StartCoroutine(LerpColor(IconsAndTicks[i], color, 0.15f));
                else
                    IconsAndTicks[i].color = color;
            else
                IconsAndTicks[i].color = color;
        }
    }

    public void SetDull()
    {
        foreach (var icon in IconsAndTicks)
        {
            HoverLeftOption(icon);
        }
        description.gameObject.SetActive(false);
    }

    public void  SetVisible()
    {
        foreach (var icon in IconsAndTicks)
        {
            HoverOverOption(icon);
        }
        description.gameObject.SetActive(true);
    }

    void HoverLeftOption(Image _image)
    {
        Color color = _image.color;
        color.a = 0.33f;
        if (HasFinishedInit)
            StartCoroutine(LerpColor(_image, color, 0.15f));
        else
            _image.color = color;
    }

    void HoverOverOption(Image _image)
    {
        Color color = _image.color;
        color.a = 1;
        if (HasFinishedInit)
            StartCoroutine(LerpColor(_image, color, 0.15f));
        else
            _image.color = color;
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
