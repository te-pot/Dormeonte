using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Colliders")]
    public Collider2D NormalDamage;
    public Collider2D CriticalDamage;

    [Header("Health Settings")]
    public float maxHealth;
    private float currentHealth;
    public float NDamage;
    public float CDamage;

    void Start()
    {
        currentHealth = maxHealth;

        // Ensure colliders are triggers
        NormalDamage.isTrigger = true;
        CriticalDamage.isTrigger = true;
    }

    public void TakeDamage(float NDamage)
    {
        currentHealth -= NDamage;
        Debug.Log($"{gameObject.name} took {NDamage} damage! Health left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeCriticalDamage(float CDamage)
    {
        currentHealth -= CDamage;
        Debug.Log($"{gameObject.name} took CRITICAL {CDamage} damage! Health left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated!");
        Destroy(gameObject);
    }
}
