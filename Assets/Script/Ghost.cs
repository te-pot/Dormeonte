using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1f;
    private float currentHealth;

    private ScoreManager scoreManager;

    void Start()
    {
        currentHealth = maxHealth;

        // Find the ScoreManager object (must have tag "ScoreManager")
        GameObject scoreObj = GameObject.FindWithTag("ScoreManager");
        if (scoreObj != null)
        {
            scoreManager = scoreObj.GetComponent<ScoreManager>();
        }
        else
        {
            Debug.LogError("⚠️ ScoreManager not found! Make sure it has the correct tag.");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Projectile projectile = collision.collider.GetComponent<Projectile>();
        if (projectile != null)
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Add score and show in console
        if (scoreManager != null)
        {
            scoreManager.AddScore();
        }

        Destroy(gameObject);
    }
}
