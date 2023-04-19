using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    //reference to audioMixer to set volume
    public AudioMixer audioMixer;

    //reference to ResolutionDropdown menu
    public TMPro.TMP_Dropdown resolutionDropdown;

    //array of resolutions for resolutionDropdown
    Resolution[] resolutions;

    void Start() 
    {
        //Get information about available resolutions
        resolutions = Screen.resolutions;

        //get rid of the garbage already in there
        resolutionDropdown.ClearOptions();

        //create a list of strings for the dropdown menu options
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        //add string ver of all resolutions to options
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            //if at current resolution, save index to set as default
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {currentResolutionIndex = i;}
        }

        //set resolutionDropdown's options to options
        resolutionDropdown.AddOptions(options);

        //set value to current and refresh to ensure it is displayed correctly
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    //change the volume of the main audio mixer
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    //toggle fullscreen based on FullscreenToggle
    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //update resolution
    public void SetResolution(int resolutionIndex)
    {
        //get resolution
        Resolution res = resolutions[resolutionIndex];
        //set resolution
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
