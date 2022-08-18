using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject audioSelect;

    [SerializeField] TMPro.TextMeshProUGUI InsertCoin;

    Color originalColor;

    bool InsertCoinSelected = true;

    private void Start()
    {
        InsertCoinSelected = true;

    }

    private void Update()
    {
        if (InsertCoinSelected)
        {
            HoverOverOption(InsertCoin);

            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Z))
            {
                StartGame();
            }
        }
        else
        {
            HoverLeftOption(InsertCoin);
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
