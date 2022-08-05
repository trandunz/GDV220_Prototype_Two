using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Camera
    [SerializeField] GameObject Main_Camera;
    [SerializeField] float Depth;

    // Enemies
    [SerializeField] GameObject Enemy;
    [SerializeField] float EnemyDepthCounter = 0.0f;
    float EnemySpawnRate = 5.0f;
    Vector3 EnemySpawnPoint;

    // Oxygem
    [SerializeField] GameObject Oxygem;
    [SerializeField] float OxygemDepthCounter = 0.0f;
    float OxygemSpawnRate = 2.0f;
    Vector3 OxygemSpawnPoint;

    // Obstacles
    [SerializeField] GameObject Obstacle;
    [SerializeField] float ObstacleDepthCounter = 0.0f;
    float ObstacleSpawnRate = 7.0f;
    Vector3 ObstacleSpawnPoint;

    private void Start()
    {
        EnemyDepthCounter = EnemySpawnRate;
    }
    private void Update()
    {
        // Update Depth to camera position
        Depth = Main_Camera.transform.position.y;

        // Update spawnpoints
        Vector3 camPos = Main_Camera.transform.position;
        camPos.y -= 10.0f;
        EnemySpawnPoint = new Vector3(camPos.x, camPos.y, camPos.z + 10.0f);
        OxygemSpawnPoint = new Vector3(camPos.x - 8.0f, camPos.y, camPos.z + 10.0f);
        ObstacleSpawnPoint = new Vector3(camPos.x + 8.0f, camPos.y, camPos.z + 10.0f);

        if ((-Depth) >= EnemyDepthCounter)
        {
            SpawnEnemy();
        }

        if ((-Depth) >= OxygemDepthCounter)
        {
            SpawnOxygem();
        }

        if ((-Depth) >= ObstacleDepthCounter)
        {
            SpawnObstacle();
        }

        // Simulate moving down
       // Vector3 pos = Main_Camera.transform.position;
       // pos.y -= 1.0f * Time.deltaTime;
       // Main_Camera.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    void SpawnEnemy()
    {
        Instantiate(Enemy, EnemySpawnPoint, Quaternion.identity);
        EnemyDepthCounter = -Depth + EnemySpawnRate;
    }

    void SpawnOxygem()
    {
        Instantiate(Oxygem, OxygemSpawnPoint, Quaternion.identity);
        OxygemDepthCounter = -Depth + OxygemSpawnRate;
    }

    void SpawnObstacle()
    {
        Instantiate(Obstacle, ObstacleSpawnPoint, Quaternion.identity);
        ObstacleDepthCounter = -Depth + ObstacleSpawnRate;
    }
}
