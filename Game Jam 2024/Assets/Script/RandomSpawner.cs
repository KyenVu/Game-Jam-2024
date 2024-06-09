using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] ItemPrefabs; // Array of trash prefabs
    public float radius = 1f;
    public float minY = 0f; // Minimum y value for random range
    public float maxY = 1f; // Maximum y value for random range

    public float spawnInterval = 0f; // Time interval for periodic spawning, 0 for no periodic spawn
    public int maxSpawnCount = 20; // Maximum number of items to spawn, 0 for unlimited

    private int currentSpawnCount = 0;

    void Start()
    {
        if (spawnInterval > 0)
        {
            InvokeRepeating("SpawnObjectAtRandomPosition", spawnInterval, spawnInterval);
        }
    }

    void Update()
    {
        
    }

    void SpawnObjectAtRandomPosition()
    {
        if (ItemPrefabs == null || ItemPrefabs.Length == 0)
        {
            Debug.LogWarning("ItemPrefabs array is not set or empty!");
            return;
        }

        if (maxSpawnCount > 0 && currentSpawnCount >= maxSpawnCount)
        {
            Debug.Log("Max spawn count reached!");
            return;
        }

        int randomIndex = Random.Range(0, ItemPrefabs.Length); // Randomly select a prefab
        GameObject selectedPrefab = ItemPrefabs[randomIndex];

        Vector2 ranPosition2D = Random.insideUnitCircle * radius;
        float randomY = Random.Range(minY, maxY); // Generate a random y-coordinate within the specified range
        Vector3 ranPosition = new Vector3(ranPosition2D.x, randomY, ranPosition2D.y) + transform.position;
        Instantiate(selectedPrefab, ranPosition, Quaternion.identity);
        currentSpawnCount++;
        Debug.Log($"Random Spawn Position: {ranPosition}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
