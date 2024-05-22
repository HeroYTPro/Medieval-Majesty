using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    Animator animator;
    public GameObject enemy; // ������ �� ������ ����������
    public MusicPlayer musicPlayer; // ������ �� ������ MusicPlayer
    public bool lastLevel;

    private void Start()
    {
        animator = GetComponent<Animator>(); // �������� ��������� Animator �� ������� FinishPoint
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!lastLevel)
            {
                UnlockNewLevel();
                StopMusicPlayer(); // ��������� ������ �� MusicPlayer
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
        // ���������, ���� ������ ���������� ��� ���������, ���������� �������� bool "Activate" � ���������
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
