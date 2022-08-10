using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public GameObject audioSelect;

    public void StartGame()
    {
        Destroy(Instantiate(audioSelect), 2.0f);
        LevelLoader.instance.LoadLevel(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleMenu(GameObject _menu)
    {
        _menu.SetActive(!_menu.activeSelf);
    }
}
