using UnityEngine;

public class EnemyGridPlacer : MonoBehaviour
{
    public int rows = 6; // ���������� ����� � �����
    public int cols = 5; // ���������� �������� � �����
    public GameObject[] enemyPrefabs; // ������ �������� ����� �����������
    public float spacing = 2f; // ���������� ����� ������������

    private int currentPrefabIndex = 0; // ������ �������� �������

    void Start()
    {
        PlaceEnemies();
    }

    void PlaceEnemies()
    {
        int totalCells = rows * cols;
        int prefabCount = enemyPrefabs.Length;

        for (int i = 0; i < totalCells; i++)
        {
            int row = i / cols; // ��������� ������� ���
            int col = i % cols; // ��������� ������� �������

            // ���� ���������� �����������, ���������� ����������
            if (currentPrefabIndex >= prefabCount)
                break;

            // ������������ ������� ��� ������� ���������� � �����
            Vector3 spawnPos = new Vector3(col * spacing, row * spacing, 0f);
            // �������� ������� ������ ��� �������� ����������
            GameObject enemyPrefab = enemyPrefabs[currentPrefabIndex];
            // ������� ��������� ����������
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            // ��������� ��������� ��������� � �������� ������ �������� ������� (����� �������� �� ������ ��������������)
            enemy.transform.parent = transform;

            currentPrefabIndex++;
        }
    }
}
