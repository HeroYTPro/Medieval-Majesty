using UnityEngine;

public class EnemyGridPlacer : MonoBehaviour
{
    public int rows = 6; // Количество строк в сетке
    public int cols = 5; // Количество столбцов в сетке
    public GameObject[] enemyPrefabs; // Массив префабов ваших противников
    public float spacing = 2f; // Расстояние между противниками

    private int currentPrefabIndex = 0; // Индекс текущего префаба

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
            int row = i / cols; // Вычисляем текущий ряд
            int col = i % cols; // Вычисляем текущий столбец

            // Если противники закончились, прекращаем размещение
            if (currentPrefabIndex >= prefabCount)
                break;

            // Рассчитываем позицию для каждого противника в сетке
            Vector3 spawnPos = new Vector3(col * spacing, row * spacing, 0f);
            // Получаем текущий префаб для создания противника
            GameObject enemyPrefab = enemyPrefabs[currentPrefabIndex];
            // Создаем экземпляр противника
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            // Добавляем созданный противник в дочерний объект текущего объекта (можно изменить на другое местоположение)
            enemy.transform.parent = transform;

            currentPrefabIndex++;
        }
    }
}
