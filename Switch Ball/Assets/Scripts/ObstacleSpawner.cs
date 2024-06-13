using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public Color[] colors;
    public float initialSpawnInterval = 2f; // ��������� �������� ����� ���������� �����������
    public float minSpawnInterval = 0.5f; // ����������� �������� ����� ���������� �����������
    public float spawnYMin = -4f; // ����������� Y-������� ��� ��������� �����������
    public float spawnYMax = 4f; // ������������ Y-������� ��� ��������� �����������
    public float spawnXLeft = -10f; // X-������� ��� ��������� ����������� �����
    public float spawnXRight = 10f; // X-������� ��� ��������� ����������� ������
    public float initialObstacleSpeed = 2f; // ��������� �������� �������� �����������
    public float maxObstacleSpeed = 10f; // ������������ �������� �������� �����������
    public float difficulty = 0.1f; // ����������� ���������� ���������
    private float spawnInterval; // �������� ����� ���������� �����������
    private float obstacleSpeed; // �������� �������� �����������
    private float timer; // ������� ����� ���������� �����������

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

        // ���������� ��������� �� ��������
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

        // ����� ���������� ������� �� ������� � ��������� ��������� �����
        GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        SpriteRenderer renderer = newObstacle.GetComponent<SpriteRenderer>();
        renderer.color = colors[Random.Range(0, colors.Length)];


        // ��������� ����������� �������� �����������
        ObstacleMovement movement = newObstacle.GetComponent<ObstacleMovement>();
        if (movement == null)
        {
            movement = newObstacle.AddComponent<ObstacleMovement>();
        }
        movement.speed = obstacleSpeed;
        movement.direction = spawnLeft ? Vector3.right : Vector3.left;
    }
}