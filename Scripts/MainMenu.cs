using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Menu objects")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject firstMainMenu;

    [Header("Levels objects")]
    [SerializeField] private GameObject LevelsPanel;
    [SerializeField] private GameObject firstLevels;

    [Header("Settings objects")]
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject firstSettings;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>(); // Получаем компонент Animator на объекте FinishPoint
        MainMenuPanel();
    }

    public void Play()
    {
        mainMenu.SetActive(false);
        LevelsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstLevels);
        //SceneController.instance.NextLevel();
        //SceneManager.LoadSceneAsync(1);
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        SettingsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstSettings);
    }

    public void MainMenuPanel()
    {
        SettingsPanel.SetActive(false);
        LevelsPanel.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(firstMainMenu);
    }

    public void Quit()
    {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
                    Debug.Log(this.name + " : " + this.GetType() + " : " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        #endif

        #if (UNITY_EDITOR)
                    UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE)
                    Application.Quit();
        #elif (UNITY_WEBGL)
                    SceneManager.LoadScene("QuitScene");
        #endif
    }
}
