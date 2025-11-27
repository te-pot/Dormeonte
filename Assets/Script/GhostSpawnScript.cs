using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GhostSpawnScript : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public GameObject ghost1;
    public GameObject ghost2;
    public GameObject ghost3;

    [Header("Spawn Settings")]
    public float spawnInterval = 3f;
    public Animator cameraAnimator; // assign this in the inspector

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

        Bounds bounds = spawnAreaCollider.bounds;

        Vector3 randomPos = new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0f
        );

        // instantiate the ghost and store the instance
        GameObject spawnedGhost = Instantiate(ghostToSpawn, randomPos, Quaternion.identity);

        // assign the camera animator to the spawned ghost's EnemyApproaches script
        EnemyApproaches ea = spawnedGhost.GetComponent<EnemyApproaches>();
        if (ea != null)
        {
            ea.cameraAnimator = cameraAnimator;
        }
        else
        {
            Debug.LogWarning($"Spawned ghost '{spawnedGhost.name}' has no EnemyApproaches component!");
        }

        Debug.Log($"Spawned: {spawnedGhost.name} at {randomPos}");
    }
}
