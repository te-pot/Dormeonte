using UnityEngine;
using System.Collections;


public class GhostBehavior : MonoBehaviour
{
    [Header("Fade Settings")]
    public float fadeDuration = 1f;

    [Header("Movement Settings")]
    public float moveRadius = 3f;      // Max distance from spawn point
    public float moveSpeed = 1f;       // Units per second
    public float idleTime = 1f;        // Pause between movements

    private Vector3 spawnPosition;
    private Vector3 targetPosition;
    private SpriteRenderer spriteRenderer;
    private bool isMoving = false;

    void Start()
    {
        spawnPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Start fully transparent
        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;

        // Fade in
        StartCoroutine(FadeIn());

        // Start moving
        SetNewTargetPosition();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color c = spriteRenderer.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            spriteRenderer.color = c;
            yield return null;
        }

        c.a = 1f;
        spriteRenderer.color = c;
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.05f)
        {
            isMoving = false;
            Invoke(nameof(SetNewTargetPosition), idleTime);
        }
    }

    private void SetNewTargetPosition()
    {
        // Random position within a circle around spawn point
        Vector2 randomOffset = Random.insideUnitCircle * moveRadius;
        targetPosition = spawnPosition + new Vector3(randomOffset.x, randomOffset.y, 0f);
        isMoving = true;
    }
}
