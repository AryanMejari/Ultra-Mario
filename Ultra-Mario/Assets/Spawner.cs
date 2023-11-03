using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpawnInterval = 2f;
    public float obstacleSpeed = 1f;

    private float timeUntilObstacleSpawn;

    private void Update()
    {
        SpawnLoop();
    }

    private void SpawnLoop()
    {
        timeUntilObstacleSpawn += Time.deltaTime;

        if (timeUntilObstacleSpawn >= obstacleSpawnInterval)
        {
            Spawn();
            timeUntilObstacleSpawn = 0f;
        }
    }

    private void Spawn()
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        // Calculate a random position within the screen's boundaries
        float randomX = Random.Range(-5f, 5f); // Adjust the range as needed
        float randomY = Random.Range(-3f, 3f); // Adjust the range as needed

        Vector3 spawnPosition = new Vector3(randomX, randomY, 0f);

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * obstacleSpeed;
    }
}
