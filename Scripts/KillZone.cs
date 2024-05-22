using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // ����������, ����� ������ ������ ������ � ���������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �������� ��������� Damageable � �������, ������� ����� � ����
        Damageable damageable = collision.GetComponent<Damageable>();

        // ���� ������ ����� ��������� Damageable, �� �������� ����� ��������
        if (damageable != null)
        {
            damageable.Kill();
        }
    }
}
