using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject cat;
    public float shootForce = 10f;

    private GameObject currentProjectile;
    private bool reloading;


    private void Start()
    {
        reloading = false;
        cat.SetActive(true);
    }
    void Update()
    {
        // Only allow shooting when there's no active projectile
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && currentProjectile == null)
        {
            Shoot();
        }

        if (currentProjectile != null)
        {
            reloading = true;
        }
        else
        {
            reloading = false;
        }

            CatRespawn();
    }

    void Shoot()
    {
        // Create projectile and store reference
        currentProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);
        }
    }

    // Called from projectile when it is destroyed
    public void ClearProjectile()
    {
        currentProjectile = null;
    }

    public void CatRespawn()
    {
        if (reloading == true)
        {
            cat.SetActive(false);
        }
        else
        {
            cat.SetActive(true);
        }
    }
}
