using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // �����, ���������� ��� ������� �� ������ "������"
    public void Restart()
    {
        // ��������� ��������� �������
        //string lastLevel = PlayerPrefs.GetString("LastLevel");
        SceneController.instance.LastLevel();
    }
    public void Exit()
    {
        SceneController.instance.MainMenu();
    }
}
