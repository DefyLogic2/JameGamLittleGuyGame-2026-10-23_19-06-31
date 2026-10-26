using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;       
    public Transform spawnPoint;     
    public bool spawnOnce = true;    

    private bool hasSpawned = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (spawnOnce && hasSpawned) return;
        if (prefab == null || spawnPoint == null) return;

        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        hasSpawned = true;
    }
}