using UnityEngine;

public class PlayFallAnimation : MonoBehaviour
{
    public Animator Animator;
    private bool gotHit = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Only play once, and only if hit by an object tagged "Player"
        if (!gotHit && collision.CompareTag("Player"))
        {
            gotHit = true;
            Animator.SetTrigger("Fall");
            GetComponent<Collider2D>().enabled = false; // stop further triggers

        }
    }
}
