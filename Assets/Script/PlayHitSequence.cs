using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyHitReaction : MonoBehaviour
{
    public Animator animator;
    private int hitTriggerID;
    private bool isHit;
    public float hitCooldown = 0.3f;

    void Start()
    {
        animator = GetComponent<Animator>();
        hitTriggerID = Animator.StringToHash("GotHit");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            PlayHitAnimation();
            ScoreManager.Instance?.AddMultipleScore(1);

        }
    }

    private void PlayHitAnimation()
    {
        if (isHit) return; // Prevent double triggers in a short time
        isHit = true;

        animator.ResetTrigger(hitTriggerID);
        animator.SetTrigger(hitTriggerID);

        Invoke(nameof(ResetHit), hitCooldown);
    }

    private void ResetHit() => isHit = false;
}
