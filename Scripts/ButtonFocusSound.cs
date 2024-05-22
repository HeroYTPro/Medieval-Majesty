using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFocusSound : MonoBehaviour, ISelectHandler
{
    public AudioClip focusSound; // �������� ���� ��� ������
    private AudioSource audioSource;

    void Start()
    {
        // �������� ��������� AudioSource
        audioSource = GetComponent<AudioSource>();
    }

    // ����� ��� ��������������� ����� ��� ��������� ������ �������
    public void OnSelect(BaseEventData eventData)
    {
        audioSource.PlayOneShot(focusSound);
    }
}
