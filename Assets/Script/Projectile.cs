using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Collider2D respawnBorder;  // Store the collider reference

    void Start()
    {
        // Find the border GameObject by tag and get its Collider2D
        GameObject borderObj = GameObject.FindWithTag("Boarder");
        if (borderObj != null)
        {
            respawnBorder = borderObj.GetComponent<Collider2D>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other == respawnBorder)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 🎯 Only play bounce sound if the hit object has the right tag
        if (collision.collider.CompareTag("Enemy") || collision.collider.CompareTag("Interior"))
        {
            // Play the bounce sound from AudioManager
            AudioManager.Instance?.PlayBounce();

            // Optional: vary bounce volume by impact strength
            // float impactForce = collision.relativeVelocity.magnitude;
            // float volume = Mathf.Clamp01(impactForce / 10f);
            // AudioManager.Instance?.PlaySFX(AudioManager.Instance.bounce, volume);
        }
    }
}
