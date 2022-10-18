using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject audioSelect;

    Color originalColor;

    bool InsertCoinSelected = true;
    public bool pauseStart = false;

    private void Start()
    {
        InsertCoinSelected = true;

    }

    private void Update()
    {
        if (InsertCoinSelected)
        {
        }
    }

    public void StartGame()
    {
        Destroy(Instantiate(audioSelect), 2.0f);
        LevelLoader.instance.LoadLevel(1);
    }

    public void ToggleMenu(GameObject _menu)
    {
        _menu.SetActive(!_menu.activeSelf);
    }

    public void HoverOverOption(TMPro.TextMeshProUGUI _text)
    {
        originalColor = _text.color;
        Color color = new Color(255, 255, 255);
        _text.color = color;
    }

    public void HoverOverOption(Image _text)
    {
        originalColor = _text.color;
        Color color = new Color(255, 255, 255);
        _text.color = color;
    }

    public void HoverLeftOption(TMPro.TextMeshProUGUI _text)
    {
        Debug.Log("Left Option");
        Color color = originalColor;
        _text.color = color;
    }

    public void HoverLeftOption(Image _text)
    {
        Debug.Log("Left Option");
        Color color = originalColor;
        _text.color = color;
    }
}
