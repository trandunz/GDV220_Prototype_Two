using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown ResolutionsDropdown;
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider StereoPanSlider;
    //List<UnityEngine.Resolution> AvailableResolutions = new List<UnityEngine.Resolution>();

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
}
