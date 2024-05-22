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
        // ���� pauseMenu �������, ��������� ���
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1; // ������������ ����� � ����
        }
        else // ���� pauseMenu �� �������, ���������� ���
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0; // ������������ ����� � ����
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
        // �������� ����� SelectedLevel � SceneController ��� �������� �������� ����
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
