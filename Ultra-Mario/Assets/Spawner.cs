using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    public float obstacleSpeed = 1f;
    public int obstaclesPerBatch = 5;
    public float batchDistance = 5f;
    public Transform selectionIcon; // Reference to your selection icon GameObject

    private void Start()
    {
        SpawnBatches();
    }

    private void SpawnBatches()
    {
        if (selectionIcon == null)
        {
            Debug.LogError("Selection Icon is not assigned. Please assign it in the Inspector.");
            return;
        }

        Vector3 spawnPosition = selectionIcon.position;

        for (int i = 0; i < obstaclesPerBatch; i++)
        {
            Spawn(spawnPosition);
            spawnPosition.x += batchDistance; // Increase the X position for the next obstacle
        }
    }

    private void Spawn(Vector3 spawnPosition)
    {
        GameObject obstacleToSpawn = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];

        GameObject spawnedObstacle = Instantiate(obstacleToSpawn, spawnPosition, Quaternion.identity);

        Rigidbody2D obstacleRB = spawnedObstacle.GetComponent<Rigidbody2D>();
        obstacleRB.velocity = Vector2.left * obstacleSpeed;
    }
}
