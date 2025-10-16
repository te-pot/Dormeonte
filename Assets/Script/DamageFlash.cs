using UnityEngine;
using System.Collections;

public class DamageFlash : MonoBehaviour
{
    [Header("Flash Settings")]
    public CanvasGroup canvasGroup;      // Assign parent CanvasGroup in inspector
    public float flashDuration = 0.2f;   // How long the flash lasts

    void Awake()
    {
        // If not assigned, try to get it from this object
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0f; // Hide initially
    }

    public void Flash()
    {
        // Stop any ongoing flash
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        canvasGroup.alpha = 1f;                   // Show all children
        yield return new WaitForSeconds(flashDuration);
        canvasGroup.alpha = 0f;                   // Hide again
    }
}
