using UnityEngine;
using UnityEngine.UI;

public class FlipUIImage : MonoBehaviour
{
    [Header("UI Image to Flip")]
    public Image targetImage;

    private bool isFlipped = false;

    // Call this function to toggle horizontal flip
    public void ToggleFlip()
    {
        if (targetImage == null) return;

        RectTransform rt = targetImage.rectTransform;
        Vector3 scale = rt.localScale;

        scale.x = isFlipped ? 1f : -1f; // flip horizontally
        rt.localScale = scale;

        isFlipped = !isFlipped;
    }

    // Optional: immediately flip on start
    private void Start()
    {
        ToggleFlip(); // uncomment if you want it flipped immediately
    }
}
