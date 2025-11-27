using UnityEngine.UI;
using UnityEngine;

public class FloatingTime_l : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float fadeDuration = 0.3f;

    private Text text;
    private RectTransform rectTransform; // ✅ Add this
    private Color startColor;
    private float timer = 0f;

    void Awake()
    {
        text = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>(); // ✅ Get RectTransform

        if (text != null)
        {
            startColor = text.color;
        }
    }

    void Start()
    {
        // Optional: add random offset here if needed
        // float randomX = Random.Range(-20f, 20f);
        // rectTransform.anchoredPosition += new Vector2(randomX, 0);
    }

    void Update()
    {
        // ✅ Move upward using anchoredPosition instead of localPosition
        //if (rectTransform != null)
        //{
          //  rectTransform.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;
        //}

        // Fade out
        timer += Time.deltaTime;
        //float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

        //if (text != null)
        //{
          //  text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        //}

        // Destroy when fully faded
        if (timer >= fadeDuration)
        {
            Destroy(gameObject);
        }
    }

    /*
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
    */
}
