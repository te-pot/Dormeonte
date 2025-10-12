using UnityEngine;
using System.Linq;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float damage = 5f;
    public float lifetime = 5f;
    public float hitRadius = 0.2f;
    public float directionTolerance = 0.5f;

    private bool hasHit = false;

    void Start()
    {
        Invoke(nameof(SelfDestruct), lifetime);
    }

    void Update()
    {
        Vector2 move = transform.up * speed * Time.deltaTime;
        Vector2 startPos = transform.position;
        Vector2 endPos = startPos + move;

        // Move projectile
        transform.position = endPos;

        // Cast a circle along the movement path to detect all ghosts
        // CircleCastAll along the movement path
        RaycastHit2D[] hits = Physics2D.CircleCastAll(startPos, hitRadius, move.normalized, move.magnitude);

        var ghostHits = hits
            .Select(h => new
            {
                Ghost = h.collider.GetComponent<Ghost>(),
                Collider = h.collider
            })
            .Where(x => x.Ghost != null)
            .ToList();

        if (ghostHits.Count == 0) return;

        // Filter by forward direction
        Vector2 forwardDir = move.normalized;
        ghostHits = ghostHits
            .Where(x =>
            {
                Vector2 toGhost = (Vector2)x.Ghost.transform.position - startPos;
                return Vector2.Dot(forwardDir, toGhost.normalized) > directionTolerance;
            })
            .ToList();

        if (ghostHits.Count == 0) return;

        // Pick the ghost with the highest layer
        var topGhostHit = ghostHits
            .OrderByDescending(x => x.Ghost.GetComponent<EnemyApproaches>().currentLayer)
            .First();

        // Determine which collider was hit
        if (topGhostHit.Collider == topGhostHit.Ghost.CriticalDamage)
        {
            topGhostHit.Ghost.TakeCriticalDamage(topGhostHit.Ghost.CDamage);
        }
        else
        {
            topGhostHit.Ghost.TakeDamage(topGhostHit.Ghost.NDamage);
        }

        // Mark hit and destroy projectile
        hasHit = true;
        NotifyShooter();
        Destroy(gameObject);


    }

    void SelfDestruct()
    {
        NotifyShooter();
        Destroy(gameObject);
    }

    void NotifyShooter()
    {
        ArrowShooter shooter = FindFirstObjectByType<ArrowShooter>();
        if (shooter != null)
            shooter.ClearProjectile();
    }
}
