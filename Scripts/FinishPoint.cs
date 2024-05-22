using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    Animator animator;
    public GameObject enemy; // Ссылка на объект противника
    public MusicPlayer musicPlayer; // Ссылка на скрипт MusicPlayer
    public bool lastLevel;

    private void Start()
    {
        animator = GetComponent<Animator>(); // Получаем компонент Animator на объекте FinishPoint
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!lastLevel)
            {
                UnlockNewLevel();
                StopMusicPlayer(); // Остановка музыки из MusicPlayer
                SceneController.instance.NextLevel();
            }
            else
            {
                StopMusicPlayer();
                SceneController.instance.GameWin();
            }
        }
    }

    private void Update()
    {
        // Проверяем, если объект противника был уничтожен, активируем значение bool "Activate" в аниматоре
        if (!enemy || enemy.activeSelf == false)
        {
            animator.SetBool("Active", true);
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >=PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
    void StopMusicPlayer()
    {
        if (musicPlayer != null)
        {
            musicPlayer.StopMusic();
        }
    }
}
