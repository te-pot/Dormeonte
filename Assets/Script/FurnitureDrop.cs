using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(HingeJoint2D))]
public class FurnitureDrop : MonoBehaviour
{
    [Header("References")]
    public GameObject groundObject;             // Ground object to fall on

    [Header("Settings")]
    public int hitsToFall = 3;                  // Hits required to fall
    public float swingForce = 1.5f;             // Impulse applied per hit
    public string projectileLayerName = "Projectile"; // Projectile layer
    public string fallenLayerName = "FallenFurniture"; // Layer after falling

    private int hitCount = 0;
    private bool hasFallen = false;

    private HingeJoint2D hinge;
    private Rigidbody2D rb;
    private Collider2D furnitureCollider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        furnitureCollider = GetComponent<Collider2D>();

        if (groundObject == null)
            Debug.LogError("⚠️ Ground object not assigned!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasFallen) return;

        if (collision.gameObject.layer == LayerMask.NameToLayer(projectileLayerName))
        {
            hitCount++;
            ScoreManager.Instance?.AddMultipleScore(1);

            Debug.Log($"{gameObject.name} hit {hitCount}/{hitsToFall} by {collision.gameObject.name}");

            Vector2 impulseDir = collision.relativeVelocity.normalized;
            rb.AddForce(impulseDir * swingForce, ForceMode2D.Impulse);

            if (hitCount >= hitsToFall)
            {
                hasFallen = true;
                StartCoroutine(FallNextPhysicsStep());
            }
        }
    }

    private IEnumerator FallNextPhysicsStep()
    {
        yield return new WaitForFixedUpdate();

        if (hinge != null)
        {
            hinge.enabled = false;
            hinge.connectedBody = null;
            ScoreManager.Instance?.AddMultipleScore(2);
            Debug.Log($"{gameObject.name} hinge disabled!");
        }

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = Mathf.Abs(rb.gravityScale);

        int fallenLayer = LayerMask.NameToLayer(fallenLayerName);
        if (fallenLayer != -1)
        {
            gameObject.layer = fallenLayer;
        }
        else
        {
            Debug.LogWarning($"Layer '{fallenLayerName}' not found! Please create it in Unity.");
        }

        if (groundObject != null)
        {
            Collider2D groundCollider = groundObject.GetComponent<Collider2D>();
            if (groundCollider != null)
            {
                Physics2D.IgnoreCollision(furnitureCollider, groundCollider, false);
            }
        }
    }
}
