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
        // Check if the projectile hit the border
        if (other == respawnBorder)
        {
            Destroy(gameObject);
        }
    }
}
