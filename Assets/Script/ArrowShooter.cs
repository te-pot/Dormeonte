using UnityEngine;
using System.Collections;

public class ArrowShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootForce = 10f;
    public float reloadTime = 2f;

    private GameObject currentProjectile;
    private bool isReloading = false;

    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            && !isReloading && currentProjectile == null)
        {
            Shoot();
        }

    }

    void Shoot()
    {
        currentProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody2D rb = currentProjectile.GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * shootForce, ForceMode2D.Impulse);

        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    public void ClearProjectile()
    {
        currentProjectile = null;
    }
}
