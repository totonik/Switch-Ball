using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Color[] colors;
    public float initialSpawnInterval = 2f; // Ќачальный интервал между генерацией преп€тствий
    public float minSpawnInterval = 0.5f; // ћинимальный интервал между генерацией преп€тствий
    public float spawnYMin = -4f; // ћинимальна€ Y-позици€ дл€ генерации преп€тствий
    public float spawnYMax = 4f; // ћаксимальна€ Y-позици€ дл€ генерации преп€тствий
    public float spawnXLeft = -10f; // X-позици€ дл€ генерации преп€тствий слева
    public float spawnXRight = 10f; // X-позици€ дл€ генерации преп€тствий справа
    public float initialObstacleSpeed = 2f; // Ќачальна€ скорость движени€ преп€тствий
    public float maxObstacleSpeed = 10f; // ћаксимальна€ скорость движени€ преп€тствий
    public float difficulty = 0.1f; //  оэффициент увеличени€ сложности
    private float spawnInterval; // »нтервал между генерацией преп€тствий
    private float obstacleSpeed; // —корость движени€ преп€тствий
    private float timer; // ¬ремен€ между генерацией преп€тствий

    void Start()
    {
        spawnInterval = initialSpawnInterval;
        obstacleSpeed = initialObstacleSpeed;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;
        }

        // ”величение сложности со временем
        float timeSinceStart = Time.timeSinceLevelLoad;
        spawnInterval = Mathf.Max(minSpawnInterval, initialSpawnInterval - timeSinceStart * difficulty);
        obstacleSpeed = Mathf.Min(maxObstacleSpeed, initialObstacleSpeed + timeSinceStart * difficulty);
    }

    void SpawnObstacle()
    {
        bool spawnLeft = Random.Range(0, 2) == 0;
        float spawnXPosition = spawnLeft ? spawnXLeft : spawnXRight;
        float spawnYPosition = Random.Range(spawnYMin, spawnYMax);
        Vector3 spawnPosition = new Vector3(spawnXPosition, spawnYPosition, -1);

        // ¬ыбор случайного префаба из массива и случайна€ установка цвета
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        SpriteRenderer renderer = newObstacle.GetComponent<SpriteRenderer>();
        renderer.color = colors[Random.Range(0, colors.Length)];


        // ”становка направлени€ движени€ преп€тстви€
        ObstacleMovement movement = newObstacle.GetComponent<ObstacleMovement>();
        if (movement == null)
        {
            movement = newObstacle.AddComponent<ObstacleMovement>();
        }
        movement.speed = obstacleSpeed;
        movement.direction = spawnLeft ? Vector3.right : Vector3.left;
    }
}