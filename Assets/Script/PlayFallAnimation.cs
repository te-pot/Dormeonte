using UnityEngine;

public class PlayFallAnimation : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public GameObject targetObject;

    [Header("Settings")]
    public int hitsToFall = 5;

    private int hitCount = 0;
    private bool hasFallen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only count hits from the player and before falling
        if (!hasFallen && collision.CompareTag("Projectile"))
        {
            hitCount++;

            if (hitCount >= hitsToFall)
            {
                TriggerFallSequence();
            }
        }
    }

    private void TriggerFallSequence()
    {
        // Step 1: Play "gotHit" animation first
        animator.SetTrigger("gotHit");

        // Step 2: Queue the "hasFallen" trigger after a short delay
        // (Adjust 0.5f depending on your animation length)
        Invoke(nameof(FinishFalling), 0.5f);
    }

    private void FinishFalling()
    {
        if (!hasFallen)
        {
            hasFallen = true;
            animator.SetTrigger("hasFallen");

            // Disable collider so it can’t be hit anymore
            if (targetObject != null)
                targetObject.GetComponent<Collider2D>().enabled = false;
        }
    }
}
