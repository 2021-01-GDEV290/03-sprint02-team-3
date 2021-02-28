using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;

    public int maxAmmo = 15;
    public int currentAmmo;

    bool canShoot = true;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canShoot && currentAmmo > 0)
        {
            Shoot();
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            Invoke("Reload", 1f);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        currentAmmo--;
        canShoot = false;
        Invoke("TimeBeforeNextShoot", 0.2f);
    }

    void TimeBeforeNextShoot()
    {
        canShoot = true;
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
