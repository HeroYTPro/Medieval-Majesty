using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWin : MonoBehaviour
{
    public void Exit()
    {
        SceneController.instance.MainMenu();
    }
}
