using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Вызывается, когда другой объект входит в коллайдер
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Получаем компонент Damageable с объекта, который вошел в зону
        Damageable damageable = collision.GetComponent<Damageable>();

        // Если объект имеет компонент Damageable, то вызываем метод убийства
        if (damageable != null)
        {
            damageable.Kill();
        }
    }
}
