using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage = 10;
    public Vector2 knockback = new Vector2(0, 0); // Значение knockback

    // Логика нанесения урона игроку
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            bool gotHit = damageable.Hit(damage, knockback); // Передача knockback

            if (gotHit)
            {
                Debug.Log(collision.name + " Hit for " + damage);
            }
        }
    }

    // Логика исчезновения заклинания после завершения анимации
    public void FinishAnimation()
    {
        Destroy(gameObject);
    }
}
