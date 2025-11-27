using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float fadeDuration = 1f;
    private TextMeshProUGUI text; // Changed from TextMeshPro
    private Color startColor;
    private float timer = 0f;

    void Awake() // Changed from Start to Awake
    {
        text = GetComponent<TextMeshProUGUI>(); // Changed component type
        if (text != null)
        {
            startColor = text.color;
        }
    }

    void Start()
    {
        float randomX = Random.Range(-20f, 20f);
        transform.localPosition += new Vector3(randomX, 0, 0); // Use localPosition for UI
    }

    void Update()
    {
        // Move upward
        transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;

        // Fade out
        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

        if (text != null)
        {
            text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }

        // Destroy when fully faded
        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string value)
    {
        if (text == null)
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        if (text != null)
        {
            text.text = value;
        }
    }
}