using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUpgradePanel : MonoBehaviour
{
    [SerializeField] UpgradeDescription description;

    int Level = 0;
    Image[] IconsAndTicks;

    private void Awake()
    {
        IconsAndTicks = GetComponentsInChildren<Image>();
        description.SetUpgradePanel(this);
        UpdateLevel();
    }

    public void Upgrade()
    {
        if (Level < 5)
        {
            Level++;
            UpdateLevel();
        }
    }

    void UpdateLevel()
    {
        SetGray();
        for (int i= Level + 1; i >= 0; i--)
        {
            Color color = IconsAndTicks[i].color;
            color.r = 1.0f;
            color.g = 1.0f;
            color.b = 1.0f;
            IconsAndTicks[i].color = color;
        }
    }

    void SetGray()
    {
        for (int i = 1; i <= 6; i++)
        {
            Color color = IconsAndTicks[i].color;
            color.r = 0.66f;
            color.g = 0.66f;
            color.b = 0.66f;
            IconsAndTicks[i].color = color;
        }
    }

    public void SetDull()
    {
        foreach (var icon in IconsAndTicks)
        {
            Color color = icon.color;
            color.a = 0.33f;
            icon.color = color;
        }
        description.gameObject.SetActive(false);
    }

    public void  SetVisible()
    {
        foreach (var icon in IconsAndTicks)
        {
            Color color = icon.color;
            color.a = 1.0f;
            icon.color = color;
        }
        description.gameObject.SetActive(true);
    }
}
