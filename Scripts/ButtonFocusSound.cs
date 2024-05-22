using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFocusSound : MonoBehaviour, ISelectHandler
{
    public AudioClip focusSound; // Звуковой файл при фокусе
    private AudioSource audioSource;

    void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // Метод для воспроизведения звука при получении фокуса кнопкой
    public void OnSelect(BaseEventData eventData)
    {
        audioSource.PlayOneShot(focusSound);
    }
}
