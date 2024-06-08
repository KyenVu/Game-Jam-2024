using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float radius = 1f;
    public float minY = 0f; // Minimum y value for random range
    public float maxY = 1f; // Maximum y value for random range
    public float minSpawnInterval = 1f; // Minimum interval in seconds between spawns
    public float maxSpawnInterval = 5f; // Maximum interval in seconds between spawns
    void Start()
    {
        // Start the coroutine to spawn objects
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObjectAtRandomPosition();
            // Generate a random interval between minSpawnInterval and maxSpawnInterval
            float spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    void SpawnObjectAtRandomPosition()
    {
        Vector2 ranPosition2D = Random.insideUnitCircle * radius;
        float randomY = Random.Range(minY, maxY); // Generate a random y-coordinate within the specified range
        Vector3 ranPosition = new Vector3(ranPosition2D.x, randomY, ranPosition2D.y) + new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(ItemPrefab, ranPosition, Quaternion.identity);
        Debug.Log($"Random Spawn Position: {ranPosition}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
