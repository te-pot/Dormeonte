using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResidualImage : MonoBehaviour
{
    [Header("Trail Settings")]
    public float spawnInterval = 0.05f;  // how often to spawn trail parts
    public float lifetime = 0.5f;        // how long each trail part lives
    public float fadeSpeed = 5f;         // how quickly trail fades

    private SpriteRenderer mainRenderer;
    private bool isDestroyed = false;

    // Track all spawned trail parts
    private List<GameObject> trailParts = new List<GameObject>();

    void Start()
    {
        mainRenderer = GetComponent<SpriteRenderer>();
        if (mainRenderer == null)
        {
            Debug.LogError("ResidualImage: No SpriteRenderer found on this object!");
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(SpawnTrail), 0, spawnInterval);
    }

    void OnDestroy()
    {
        // Stop spawning trails
        isDestroyed = true;
        CancelInvoke(nameof(SpawnTrail));

        // Destroy all existing trail parts immediately
        foreach (GameObject part in trailParts)
        {
            if (part != null)
                Destroy(part);
        }
        trailParts.Clear();
    }

    void SpawnTrail()
    {
        if (isDestroyed) return;

        GameObject trailPart = new GameObject("TrailPart");
        SpriteRenderer sr = trailPart.AddComponent<SpriteRenderer>();

        // Copy properties from the projectile
        sr.sprite = mainRenderer.sprite;
        sr.color = mainRenderer.color;
        sr.sortingLayerID = mainRenderer.sortingLayerID;
        sr.sortingOrder = mainRenderer.sortingOrder - 1;

        // Match transform
        trailPart.transform.position = transform.position;
        trailPart.transform.rotation = transform.rotation;
        trailPart.transform.localScale = transform.localScale;

        // Keep track of it
        trailParts.Add(trailPart);

        // Fade & destroy
        StartCoroutine(FadeAndDestroy(sr, lifetime));
    }

    IEnumerator FadeAndDestroy(SpriteRenderer sr, float life)
    {
        float elapsed = 0f;
        Color startColor = sr.color;

        while (elapsed < life)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsed / life);
            sr.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        trailParts.Remove(sr.gameObject); // Remove from list
        Destroy(sr.gameObject);
    }
}
