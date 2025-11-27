using UnityEngine;

public class ParallaxDepth : MonoBehaviour
{
    [Header("Layers (Closest → Furthest)")]
    public RectTransform foreground;
    public RectTransform midground;
    public RectTransform background;

    [Header("Pivot Settings")]
    public RectTransform pivotPoint; // Custom pivot

    [Header("Movement Settings")]
    public float foregroundIntensity = 10f;
    public float midgroundIntensity = 20f;
    public float backgroundIntensity = 30f;
    public float maxOffset = 40f;

    private Canvas rootCanvas;
    private Vector2 screenCenter;

    void Start()
    {
        rootCanvas = GetComponentInParent<Canvas>();
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void Update()
    {
        if (rootCanvas == null) return;

        Vector2 mousePos = Input.mousePosition;

        // Convert pivot point to screen position
        Vector2 pivotPos = pivotPoint != null
            ? RectTransformUtility.WorldToScreenPoint(rootCanvas.worldCamera, pivotPoint.position)
            : screenCenter;

        // Normalize offset roughly [-1, 1]
        Vector2 offset = (mousePos - pivotPos) / screenCenter;

        ApplyParallax(foreground, offset, foregroundIntensity);
        ApplyParallax(midground, offset, midgroundIntensity);
        ApplyParallax(background, offset, backgroundIntensity);
    }

    void ApplyParallax(RectTransform layer, Vector2 offset, float intensity)
    {
        if (layer == null) return;

        Vector2 movement = -offset * intensity;
        movement = Vector2.ClampMagnitude(movement, maxOffset);

        // Smooth movement (optional)
        layer.anchoredPosition = Vector2.Lerp(layer.anchoredPosition, movement, Time.deltaTime * 5f);
    }
}
