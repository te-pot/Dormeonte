using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CombinedHitandFall : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator animator;
    public string gotHitTrigger = "GotHit";   // trigger name in Animator
    public string hasFallenTrigger = "HasFallen"; // trigger name in Animator

    [Header("Hit Settings")]
    public int hitsToFall = 5;         // how many hits before falling
    public float hitCooldown = 0.3f;   // prevent multiple hits too fast

    [Header("References")]
    public Collider2D targetCollider;  // assign the collider to disable when dead
    public Collider2D secondCollider;

    private int hitCount = 0;
    private bool isHit = false;
    private bool hasFallen = false;

    private int gotHitID;
    private int hasFallenID;

    void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (targetCollider == null)
            targetCollider = GetComponent<Collider2D>();

        gotHitID = Animator.StringToHash(gotHitTrigger);
        hasFallenID = Animator.StringToHash(hasFallenTrigger);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (hasFallen) return;

        if (other.CompareTag("Projectile"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        if (isHit) return; // cooldown
        isHit = true;

        hitCount++;
        animator.ResetTrigger(gotHitID);
        animator.SetTrigger(gotHitID);

        if (hitCount >= hitsToFall)
        {
            TriggerFall();
        }

        Invoke(nameof(ResetHit), hitCooldown);
    }

    private void TriggerFall()
    {
        if (hasFallen) return;

        hasFallen = true;
        animator.ResetTrigger(hasFallenID);
        animator.SetTrigger(hasFallenID);

        // Disable collider after a small delay to allow animation start
        if (targetCollider != null)
            Invoke(nameof(DisableCollider), 0.3f);
    }

    private void DisableCollider()
    {
        targetCollider.enabled = false;
        secondCollider.enabled = false;
    }

    private void ResetHit()
    {
        isHit = false;
    }
}
