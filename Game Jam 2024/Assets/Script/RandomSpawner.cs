using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float radius = 1f;
    public float minY = 0f; // Minimum y value for random range
    public float maxY = 1f; // Maximum y value for random range

    void Start()
    {
        // Optional: You can call SpawnObjectAtRandomPosition here if you want to spawn an item at start.
        // SpawnObjectAtRandomPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnObjectAtRandomPosition();
        }
    }

    void SpawnObjectAtRandomPosition()
    {
        Vector2 ranPosition2D = Random.insideUnitCircle * radius;
        float randomY = Random.Range(minY, maxY); // Generate a random y-coordinate within the specified range
        Vector3 ranPosition = new Vector3(ranPosition2D.x, randomY, ranPosition2D.y) + new Vector3(transform.position.x, 0f, transform.position.z);
        Instantiate(ItemPrefab, ranPosition, Quaternion.identity);
        Debug.Log($"Random Spawn Position: {ranPosition}");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
