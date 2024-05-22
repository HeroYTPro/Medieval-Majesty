using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public float floatAmplitude = 0.1f; // ��������� �������� ��������
    public float floatSpeed = 5f; // �������� �������� ��������
    //public Vector3 spinRotationSpeed = new Vector3 (0, 180, 0);

    AudioSource pickupSource;
    Vector3 startPosition;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
        startPosition = transform.position; // ���������� ��������� ������� �������
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                if (pickupSource)
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        //transform.eulerAngles += spinRotationSpeed * Time.deltaTime;

        // ���������� ��������� ��� �������� �������� ����� � ����
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
