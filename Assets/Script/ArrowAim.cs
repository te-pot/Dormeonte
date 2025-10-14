using UnityEngine;

public class ArrowAim : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 10f; // Smoothness of rotation

    void Update()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from arrow to mouse
        Vector3 direction = mousePosition - transform.position;
        direction.z = 0f; // Ignore z-axis (2D game)

        // Compute target angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Smoothly rotate the arrow toward the target angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle - 90f); // adjust -90° if your sprite points up
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
