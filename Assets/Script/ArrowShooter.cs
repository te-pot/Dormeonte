using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public float reloadTime = 2f; // seconds between shots

    private GameObject currentProjectile;
    private bool isReloading = false;

    void Update()
    {
        // Try to shoot when spacebar (or mouse) pressed
        if (Input.GetKeyDown(KeyCode.Space) && !isReloading && currentProjectile == null)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create projectile and store reference
        currentProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);

        // Start reloading coroutine
        StartCoroutine(Reload());
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        // Allow shooting again only if projectile has been destroyed
        isReloading = false;
    }

    // Optional: if the projectile destroys itself after hitting something
    // it can call this method on the shooter to reset the reference
    public void ClearProjectile()
    {
        currentProjectile = null;
    }
}
