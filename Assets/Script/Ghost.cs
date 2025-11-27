using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 1f;
    private float currentHealth;

    private ScoreManager scoreManager;
    public bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;

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
        Debug.Log($"Ghost collided with {collision.collider.name}");
        Projectile projectile = collision.collider.GetComponent<Projectile>();
        if (projectile != null)
        {
            Debug.Log("Projectile detected!");
            TakeDamage(1);
        }
    }


    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            //AudioManager.Instance.PlaySFX("ghostdamage"); ;
            Die();
        }
    }


    void Die()
    {
        Debug.Log("Ghost died!");
        if (scoreManager != null)
        {
            scoreManager.AddMultipleScore(5);
        }

        if (AudioManager.Instance != null)
            AudioManager.Instance.PlaySFX("ghostdeath");

        Destroy(gameObject);
    }
}
