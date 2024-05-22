using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerController playerController;
    Collider2D collider;

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        collider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            float groundHeight = collision.transform.position.y; // Получаем высоту земли
            playerController.UpdateCheckpoint(transform.position, groundHeight);
            collider.enabled = false;
        }
    }
}
