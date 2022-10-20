using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown ResolutionsDropdown;
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider StereoPanSlider;
    [SerializeField] TMPro.TextMeshProUGUI VolumeText;
    [SerializeField] TMPro.TextMeshProUGUI StereoPanText;
    [SerializeField] Image Selector;
    [SerializeField] TMPro.TextMeshProUGUI ReturnText;
    //List<UnityEngine.Resolution> AvailableResolutions = new List<UnityEngine.Resolution>();
    int MenuOptionSelection = 0;

    private void Start()
    {
        //Resolution forteenFourty = new Resolution();
        //forteenFourty.height = 1080;
        //forteenFourty.width = 1440;
        //forteenFourty.refreshRate = Screen.currentResolution.refreshRate;
        //
        //Resolution sevenTwenty = new Resolution();
        //sevenTwenty.height = 1200;
        //sevenTwenty.width = 1600;
        //sevenTwenty.refreshRate = Screen.currentResolution.refreshRate;
        
        //Resolution tenEighty = new Resolution();
        //tenEighty.height = 1440;
        //tenEighty.width = 1920;
        //tenEighty.refreshRate = Screen.currentResolution.refreshRate;
        
        //AvailableResolutions.Add(forteenFourty);
        //AvailableResolutions.Add(sevenTwenty);
        //AvailableResolutions.Add(tenEighty);
        
        //ResolutionsDropdown.ClearOptions();
        //int nativeResolutionIndex = 0;
        
        //foreach (Resolution resolution in AvailableResolutions)
        //{
        //    
        //    if (resolution.width == Screen.currentResolution.width
        //        && resolution.height == Screen.currentResolution.height)
        //    {
        //        ResolutionsDropdown.AddOptions(new List<string> {resolution.ToString()});
        //        ResolutionsDropdown.SetValueWithoutNotify(nativeResolutionIndex);
        //    }
        //    else
        //    {
        //        nativeResolutionIndex++;
        //        ResolutionsDropdown.AddOptions(new List<string> { resolution.ToString() });
        //    }
        //}
        
        //if (PlayerPrefs.GetInt("Resolution") == 0)
        //{
        //    PlayerPrefs.SetInt("Resolution", ResolutionsDropdown.value);
        //}
        //else
        //{
        //    ResolutionsDropdown.value = PlayerPrefs.GetInt("Resolution");
        //}
        if (PlayerPrefs.GetFloat("MasterVolume") == 0.0f)
        {
            PlayerPrefs.SetFloat("MasterVolume", MasterVolumeSlider.value);
        }
        else
        {
            MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.GetFloat("StereoPan") == 0.0f)
        {
            PlayerPrefs.SetFloat("StereoPan", StereoPanSlider.value);
        }
        else
        {
            StereoPanSlider.value = PlayerPrefs.GetFloat("StereoPan");
        }
        PlayerPrefs.Save();

        //UpdateResolution();
        UpdateVolume();
        UpdateStereoPan();
    }

    //public void UpdateResolution()
    //{
    //    Resolution newResolution = AvailableResolutions[ResolutionsDropdown.value];
    //    Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen, newResolution.refreshRate);
    //    PlayerPrefs.SetInt("Resolution", ResolutionsDropdown.value);
    //    PlayerPrefs.Save();
    //}

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            MenuOptionSelection--;
            if (MenuOptionSelection < 0)
                MenuOptionSelection = 2;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (MenuOptionSelection == 0)
                DecreaseVolume();

            if (MenuOptionSelection == 1)
                DecreaseSP();
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            MenuOptionSelection++;
            if (MenuOptionSelection > 2)
                MenuOptionSelection = 0;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (MenuOptionSelection == 0)
                IncreaseVolume();

            if (MenuOptionSelection == 1)
                IncreaseSP();
        }
        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.Backspace))
        {
            if (MenuOptionSelection == 2)
                Return();
        }


        if (MenuOptionSelection == 0)
            Selector.rectTransform.position = VolumeText.rectTransform.position + Vector3.left * 80.0f;

        if (MenuOptionSelection == 1)
            Selector.rectTransform.position = StereoPanText.rectTransform.position + Vector3.left * 100.0f;

        if (MenuOptionSelection == 2)
            Selector.rectTransform.position = ReturnText.rectTransform.position + Vector3.left * 80.0f;
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void UpdateVolume()
    {
        AudioListener.volume = MasterVolumeSlider.value;
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
        PlayerPrefs.Save();
    }
    public void UpdateStereoPan()
    {
        foreach(AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.panStereo = (StereoPanSlider.value * 2) - 1.0f;
        }
        PlayerPrefs.SetFloat("StereoPan", StereoPanSlider.value);
        PlayerPrefs.Save();
    }
    public void IncreaseVolume()
    {
        MasterVolumeSlider.value += 0.1f;
        UpdateVolume();
    }
    public void DecreaseVolume()
    {
        MasterVolumeSlider.value -= 0.1f;
        UpdateVolume();
    }
    public void IncreaseSP()
    {
        StereoPanSlider.value += 0.1f;
        UpdateStereoPan();
    }
    public void DecreaseSP()
    {
        StereoPanSlider.value -= 0.1f;
        UpdateStereoPan();
    }
    public void Return()
    {
        gameObject.SetActive(false);
    }
}
