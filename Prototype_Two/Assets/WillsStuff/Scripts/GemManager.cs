using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public static GemManager instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            AddGems(1000);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            RemoveGems(1000);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            ResetGemCount();
        }
    }

    public int GetGemCount()
    {
        return PlayerPrefs.GetInt("Gems");
    }

    public void ResetGemCount()
    {
        PlayerPrefs.SetInt("Gems", 0);
        PlayerPrefs.Save();
    }

    public void AddGems(int _amount)
    {
        PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + _amount);
        PlayerPrefs.Save();
    }

    public void RemoveGems(int _amount)
    {
        for (int i = _amount; PlayerPrefs.GetInt("Gems") > 0 && i > 0; i--)
        {
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") - 1);
        }
        PlayerPrefs.Save();
    }
}
