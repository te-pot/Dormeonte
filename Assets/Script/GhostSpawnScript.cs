using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GhostSpawnScript : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject ghost3;
    //public GameObject ghost4;
    //public GameObject ghost5;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;  // seconds between spawns
    private float timer;

    private GameObject[] ghostPrefabs;
    private Collider2D spawnAreaCollider;

    void Start()
    {
        spawnAreaCollider = GetComponent<Collider2D>();
        ghostPrefabs = new[] { ghost1, ghost2, ghost3 };
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnRandomGhost();
            timer = 0f;
        }
    }

    void SpawnRandomGhost()
    {
        if (ghostPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, ghostPrefabs.Length);
        GameObject ghostToSpawn = ghostPrefabs[randomIndex];

        // Get bounds of the collider
        Bounds bounds = spawnAreaCollider.bounds;

        // Pick a random point within the collider bounds
        Vector3 randomPos = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0f
        );

        Instantiate(ghostToSpawn, randomPos, Quaternion.identity);

        Debug.Log($"Spawned: {ghostToSpawn.name} at {randomPos}");
    }
}
