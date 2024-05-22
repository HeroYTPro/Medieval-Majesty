using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Метод, вызываемый при нажатии на кнопку "заново"
    public void Restart()
    {
        // Загружаем последний уровень
        //string lastLevel = PlayerPrefs.GetString("LastLevel");
        SceneController.instance.LastLevel();
    }
    public void Exit()
    {
        SceneController.instance.MainMenu();
    }
}
