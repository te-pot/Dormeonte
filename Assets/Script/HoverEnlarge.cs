using UnityEngine;
using UnityEngine.EventSystems;

public class HoverEnlarge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scale Settings")]
    public float hoverScale = 1.1f;
    public float scaleSpeed = 8f;

    [Header("Target")]
    public Transform scaleTarget; // Assign the child to scale in Inspector

    private Vector3 originalScale;
    private Vector3 targetScale;

    void Start()
    {
        // If no target assigned, scale self
        if (scaleTarget == null)
            scaleTarget = transform;

        originalScale = scaleTarget.localScale;
        targetScale = originalScale;
    }

    void Update()
    {
        // Use unscaledDeltaTime so it works even when Time.timeScale = 0
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * scaleSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * hoverScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }
}