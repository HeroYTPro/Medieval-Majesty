using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLauncher : MonoBehaviour
{
    public GameObject spellPrefab; // Заменяем projectilePrefab на spellPrefab
    private GameObject player; // Добавляем переменную для объекта игрока
    public float yOffset = 1.7f; // Здесь можно указать любое смещение по вертикали

    private void Start()
    {
        // Находим объект игрока по тегу
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CastSpell()
    {
        if (player != null)
        {
            // Получаем текущее положение игрока
            Vector3 playerPosition = player.transform.position;

            // Добавляем смещение вверх по оси Y
            //float yOffset = 1.7f; // Здесь можно указать любое смещение по вертикали
            playerPosition.y += yOffset;

            // Создаем заклинание в текущем положении игрока
            GameObject spell = Instantiate(spellPrefab, playerPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }
}
