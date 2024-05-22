using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Dropdown graphicsDropdown; // добавляем переменную для Dropdown уровня графики
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

        //// Установка Toggle в соответствии с режимом экрана
        //fullscreenToggle.isOn = Screen.fullScreen;

        // Получение сохраненных настроек разрешения и режима экрана
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        // Установка сохраненных настроек разрешения и режима экрана
        resolutionDropdown.value = savedResolutionIndex;
        fullscreenToggle.isOn = savedFullscreen;
        SetResolution(savedResolutionIndex); // Применяем разрешение

        // Установка текущего уровня качества графики в Dropdown
        int currentQualityLevel = QualitySettings.GetQualityLevel();
        graphicsDropdown.value = currentQualityLevel;
    }

    public void SetResolution(int resolutionIndex)
    {
        //Resolution resolution = resolutions[resolutionIndex];
        //Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreenToggle.isOn);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex); // Сохраняем выбранное разрешение
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityLevel", qualityIndex); // Сохраняем выбранный уровень качества
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        //Screen.fullScreen = isFullscreen;

        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0); // Сохраняем выбранный режим экрана
        PlayerPrefs.Save();
    }
}
