using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject firstPauseMenu;

    public void Pause()
    {
        // Если pauseMenu активно, отключаем его
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1; // Возобновляем время в игре
        }
        else // Если pauseMenu не активно, активируем его
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0; // Замораживаем время в игре
            EventSystem.current.SetSelectedGameObject(firstPauseMenu);
        }
    }

    //public void Home()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //    Time.timeScale = 1;
    //}
    public void Home()
    {
        // Вызываем метод SelectedLevel в SceneController для загрузки главного меню
        SceneController.instance.MainMenu();
        Time.timeScale = 1;
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
