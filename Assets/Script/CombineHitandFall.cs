using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CombinedHitandFall : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator animator;
    public string gotHitTrigger = "GotHit";   
    public string hasFallenTrigger = "HasFallen";

    [Header("Hit Settings")]
    public int hitsToFall = 5;         
    public float hitCooldown = 0.3f;  

    [Header("References")]
    public Collider2D targetCollider; 
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
            ScoreManager.Instance?.AddMultipleScore(1);
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
        ScoreManager.Instance?.AddMultipleScore(5);
        animator.ResetTrigger(hasFallenID);
        animator.SetTrigger(hasFallenID);

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
