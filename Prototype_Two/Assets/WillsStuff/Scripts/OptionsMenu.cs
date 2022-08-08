using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown ResolutionsDropdown;
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider StereoPanSlider;
    [SerializeField] GameObject KeyBindings;
    List<UnityEngine.Resolution> AvailableResolutions = new List<UnityEngine.Resolution>();

    private void Start()
    {
        Resolution tenEighty = new Resolution();
        tenEighty.height = 1080;
        tenEighty.width = 1920;
        tenEighty.refreshRate = Screen.currentResolution.refreshRate;

        Resolution sevenTwenty = new Resolution();
        sevenTwenty.height = 720;
        sevenTwenty.width = 1280;
        sevenTwenty.refreshRate = Screen.currentResolution.refreshRate;

        Resolution forteenFourty = new Resolution();
        forteenFourty.height = 1440;
        forteenFourty.width = 2560;
        forteenFourty.refreshRate = Screen.currentResolution.refreshRate;

        AvailableResolutions.Add(sevenTwenty);
        AvailableResolutions.Add(tenEighty);
        AvailableResolutions.Add(forteenFourty);

        ResolutionsDropdown.ClearOptions();
        int nativeResolutionIndex = 0;

        foreach (Resolution resolution in AvailableResolutions)
        {
            
            if (resolution.width == Screen.currentResolution.width
                && resolution.height == Screen.currentResolution.height)
            {
                ResolutionsDropdown.AddOptions(new List<string> {resolution.ToString()});
                ResolutionsDropdown.SetValueWithoutNotify(nativeResolutionIndex);
            }
            else
            {
                nativeResolutionIndex++;
                ResolutionsDropdown.AddOptions(new List<string> { resolution.ToString() });
            }
        }

        ResolutionsDropdown.value = PlayerPrefs.GetInt("Resolution");
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        StereoPanSlider.value = PlayerPrefs.GetFloat("StereoPan");

        UpdateResolution();
        UpdateVolume();
        UpdateStereoPan();

        KeyBindings.SetActive(false);
    }

    private void Awake()
    {
        ResolutionsDropdown.value = PlayerPrefs.GetInt("Resolution");
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        StereoPanSlider.value = PlayerPrefs.GetFloat("StereoPan");
    }

    private void OnEnable()
    {
        ResolutionsDropdown.value = PlayerPrefs.GetInt("Resolution");
        MasterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        StereoPanSlider.value = PlayerPrefs.GetFloat("StereoPan");
    }

    public void UpdateResolution()
    {
        Resolution newResolution = AvailableResolutions[ResolutionsDropdown.value];
        Screen.SetResolution(newResolution.width, newResolution.height, Screen.fullScreen, newResolution.refreshRate);
        PlayerPrefs.SetInt("Resolution", ResolutionsDropdown.value);
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void UpdateVolume()
    {
        AudioListener.volume = MasterVolumeSlider.value;
        PlayerPrefs.SetFloat("MasterVolume", AudioListener.volume);
    }
    public void UpdateStereoPan()
    {
        foreach(AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            audioSource.panStereo = StereoPanSlider.value;
        }
        PlayerPrefs.SetFloat("StereoPan", StereoPanSlider.value);
    }
}
