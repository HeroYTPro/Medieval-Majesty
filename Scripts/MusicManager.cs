using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource battleMusic;
    public MusicPlayer musicPlayer; // ������ �� ������ MusicPlayer
    public GameObject enemy; // ������ �� ������ ����������

    private bool isTransitioning = false;
    private bool isMusicPlaying = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            if (!isMusicPlaying)
            {
                isTransitioning = true;
                StartCoroutine(PlayBattleMusic());
                StopMusicPlayer(); // ��������� ������ �� MusicPlayer
                isMusicPlaying = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // ���������, ���� ������ ���������� ��� ���������
        if (!enemy || enemy.activeSelf == false)
        {
            isTransitioning = true;
            StartCoroutine(StopBattleMusic());
            PlayMusicPlayer();
        }
    }

    void StopMusicPlayer()
    {
        if (musicPlayer != null)
        {
            musicPlayer.StopMusic();
        }
    }

    IEnumerator PlayBattleMusic()
    {
        battleMusic.Play(); // ������������� ���������� ������

        // ���������� ����������� ��������� ���������� ������
        while (battleMusic.volume < 0.5f)
        {
            battleMusic.volume += Time.deltaTime * 0.1f;
            yield return null;
        }

        isTransitioning = false;
    }

    IEnumerator StopBattleMusic()
    {

        // ���������� ��������� ��������� ���������� ������
        while (battleMusic.volume > 0f)
        {
            battleMusic.volume -= Time.deltaTime * 0.1f;
            yield return null;
        }
        battleMusic.Stop();

        isTransitioning = false;
    }


    void PlayMusicPlayer()
    {
        if (musicPlayer != null)
        {
            musicPlayer.PlayMusic();
        }
    }
}
