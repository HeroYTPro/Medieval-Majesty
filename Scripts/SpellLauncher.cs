using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellLauncher : MonoBehaviour
{
    public GameObject spellPrefab; // �������� projectilePrefab �� spellPrefab
    private GameObject player; // ��������� ���������� ��� ������� ������
    public float yOffset = 1.7f; // ����� ����� ������� ����� �������� �� ���������

    private void Start()
    {
        // ������� ������ ������ �� ����
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void CastSpell()
    {
        if (player != null)
        {
            // �������� ������� ��������� ������
            Vector3 playerPosition = player.transform.position;

            // ��������� �������� ����� �� ��� Y
            //float yOffset = 1.7f; // ����� ����� ������� ����� �������� �� ���������
            playerPosition.y += yOffset;

            // ������� ���������� � ������� ��������� ������
            GameObject spell = Instantiate(spellPrefab, playerPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }
}
