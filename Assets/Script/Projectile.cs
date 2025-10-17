using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    private Collider2D projectileCollider;
    private Collider2D borderCollider;

    void Start()
    {
        projectileCollider = GetComponent<Collider2D>();

        // Find the border object (must be tagged "Border")
        GameObject borderObj = GameObject.FindWithTag("Boarder");
        if (borderObj != null)
        {
            borderCollider = borderObj.GetComponent<Collider2D>();
        }
        else
        {
            Debug.LogWarning("⚠️ No object with tag 'Border' found in scene!");
        }

        // Ignore all colliders tagged as "Floor"
        Collider2D[] allColliders = FindObjectsByType<Collider2D>(FindObjectsSortMode.None);
        foreach (Collider2D col in allColliders)
        {
            if (col != projectileCollider && col.CompareTag("Floor"))
            {
                Physics2D.IgnoreCollision(projectileCollider, col, true);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.tag;

        if (tag == "Enemy" || tag == "Interior")
        {
            if (AudioManager.Instance != null && AudioManager.Instance.sfxSource2 != null)
            {
                // Stop the current sound if it's playing
                AudioManager.Instance.sfxSource2.Stop();

                // Assign the clip and play from start
                AudioManager.Instance.sfxSource2.clip = AudioManager.Instance.sfx2a;
                AudioManager.Instance.sfxSource2.Play();
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Destroy projectile if it hits the border
        if (borderCollider != null && other == borderCollider)
        {
            Destroy(gameObject);
        }
    }
}
