using UnityEngine;

public class BossGhost : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1f;
    private float currentHealth;

    [Header("References")]
    public GameObject ghostObject;  // Assign the boss prefab itself in Inspector

    [Header("Score Settings")]
    public int scorePerHit = 1;       // Points per projectile hit
    public int scoreOnDeath = 100;    // Points when boss dies

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only react to projectile hits
        if (collision.collider.GetComponent<Projectile>() != null && !isDead)
        {
            TakeDamage(1f);

            // Add score for hitting the boss
            ScoreManager.Instance?.AddScore(); // Adds scorePerHit points per hit
            Debug.Log($"Projectile hit! Added {scorePerHit} points.");
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // Prevent multiple death triggers

        currentHealth -= amount;

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        // Add score for defeating the boss
        ScoreManager.Instance?.AddMultipleScore(scoreOnDeath);
        Debug.Log($"Boss defeated! Added {scoreOnDeath} points.");
        AudioManager.Instance.PlaySFX("bossdeath"); ;


        // Destroy the boss object
        if (ghostObject != null)
        {
            Destroy(ghostObject);

        }
        else
        {
            Destroy(gameObject);

        }
    }
}
