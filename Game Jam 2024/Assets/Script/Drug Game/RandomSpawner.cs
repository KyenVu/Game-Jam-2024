using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Array of prefabs to randomly choose from
    public float radius = 1f;
    public float minY = 0f; // Minimum y value for random range
    public float maxY = 1f; // Maximum y value for random range
    public float minSpawnInterval = 1f; // Minimum interval in seconds between spawns
    public float maxSpawnInterval = 5f; // Maximum interval in seconds between spawns

    public int maxSpawnCount = 0; // Maximum number of items to spawn, 0 for unlimited

    private int currentSpawnCount = 0;
    private bool maxSpawnReached = false;

    void Start()
    {
        // Start the coroutine to spawn objects
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            if (maxSpawnReached)
            {
                yield break; // Exit the coroutine if the max spawn count has been reached
            }
            SpawnObjectAtRandomPosition();
            // Generate a random interval between minSpawnInterval and maxSpawnInterval
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObjectAtRandomPosition()
    {
        if (maxSpawnCount > 0 && currentSpawnCount >= maxSpawnCount)
        {
            maxSpawnReached = true;
            Debug.Log("Max spawn count reached!");
            return;
        }

        Vector2 ranPosition2D = Random.insideUnitCircle * radius;
        float randomY = Random.Range(minY, maxY); // Generate a random y-coordinate within the specified range
        Vector3 ranPosition = new Vector3(ranPosition2D.x, randomY, ranPosition2D.y) + new Vector3(transform.position.x, transform.position.y, transform.position.z);

        // Choose a random prefab from the array
        GameObject randomPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        Instantiate(randomPrefab, ranPosition, Quaternion.identity);

        currentSpawnCount++; // Increment the current spawn count
        Debug.Log($"Random Spawn Position: {ranPosition}, Current Spawn Count: {currentSpawnCount}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
