using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource battleMusic;
    public MusicPlayer musicPlayer; // Ссылка на скрипт MusicPlayer
    public GameObject enemy; // Ссылка на объект противника

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
                StopMusicPlayer(); // Остановка музыки из MusicPlayer
                isMusicPlaying = true;
            }
        }
    }

    private void FixedUpdate()
    {
        // Проверяем, если объект противника был уничтожен
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
        battleMusic.Play(); // Воспроизводим переходную музыку

        // Постепенно увеличиваем громкость переходной музыки
        while (battleMusic.volume < 0.5f)
        {
            battleMusic.volume += Time.deltaTime * 0.1f;
            yield return null;
        }

        isTransitioning = false;
    }

    IEnumerator StopBattleMusic()
    {

        // Постепенно уменьшаем громкость переходной музыки
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
