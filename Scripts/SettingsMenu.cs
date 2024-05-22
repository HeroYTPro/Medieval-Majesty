using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown; // ��������� ���������� ��� Dropdown ������ �������
    public Toggle fullscreenToggle;

    public void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        //resolutionDropdown.value = currentResolutionIndex;
        //resolutionDropdown.RefreshShownValue();

        //// ��������� Toggle � ������������ � ������� ������
        //fullscreenToggle.isOn = Screen.fullScreen;

        // ��������� ����������� �������� ���������� � ������ ������
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        // ��������� ����������� �������� ���������� � ������ ������
        resolutionDropdown.value = savedResolutionIndex;
        fullscreenToggle.isOn = savedFullscreen;
        SetResolution(savedResolutionIndex); // ��������� ����������

        // ��������� �������� ������ �������� ������� � Dropdown
        int currentQualityLevel = QualitySettings.GetQualityLevel();
        graphicsDropdown.value = currentQualityLevel;
    }

    public void SetResolution(int resolutionIndex)
    {
        //Resolution resolution = resolutions[resolutionIndex];
        //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex); // ��������� ��������� ����������
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex); // ��������� ��������� ������� ��������
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        //Screen.fullScreen = isFullscreen;

        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0); // ��������� ��������� ����� ������
        PlayerPrefs.Save();
    }
}
