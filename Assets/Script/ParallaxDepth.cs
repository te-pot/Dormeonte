using UnityEngine;

public class ParallaxDepth : MonoBehaviour
{
    [Header("Layers (Closest → Furthest)")]
    public RectTransform foreground;
    public RectTransform midground;
    public RectTransform background;

    [Header("Movement Settings")]
    public float foregroundIntensity = 10f;
    public float midgroundIntensity = 20f;
    public float backgroundIntensity = 30f;
    public float maxOffset = 40f; // Max movement in pixels

    private Vector2 screenCenter;

    void Start()
    {
        screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 offset = (mousePos - screenCenter) / screenCenter; // normalized [-1, 1]

        // Move each layer opposite to mouse direction
        ApplyParallax(foreground, offset, foregroundIntensity);
        ApplyParallax(midground, offset, midgroundIntensity);
        ApplyParallax(background, offset, backgroundIntensity);
    }

    void ApplyParallax(RectTransform layer, Vector2 offset, float intensity)
    {
        if (layer == null) return;

        // Move in opposite direction for depth illusion
        Vector2 movement = -offset * intensity;

        // Clamp the movement
        movement = Vector2.ClampMagnitude(movement, maxOffset);

        layer.anchoredPosition = movement;
    }
}
