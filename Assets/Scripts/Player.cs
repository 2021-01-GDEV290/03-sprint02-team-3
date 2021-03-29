using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Controller")]
    public float moveSpeed;
    public Camera cam;
    private float xInput;
    private float yInput;
    private Rigidbody2D rb;
    private Vector2 mousePos;

    [Header("Health Tracker")]
    public int maxHealth;
    public int health;

    [Header("Shooting")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public float bulletForce = 20f;
    public int maxAmmo = 15;
    public int currentAmmo;
    bool canShoot = true;

    [Header("Point Tracker")]
    public static int points;
    public int trackedPoints; // For testing purposes only

    // Start is called before the first frame update
    void Start()
    {
        // Player Movement
        rb = GetComponent<Rigidbody2D>();
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // Shooting
        currentAmmo = maxAmmo;

        // Health
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Player Movement
        xInput = Input.GetAxisRaw("Horizontal") * moveSpeed;
        yInput = Input.GetAxisRaw("Vertical") * moveSpeed;

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Shooting
        if (Input.GetButtonDown("Fire1") && canShoot && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
        {
            animator.SetBool("Shoot", true);
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            canShoot = false;
            Invoke("Reload", 1f);
        }

        // Points
        trackedPoints = points;
    }

    private void FixedUpdate()
    {
        // Player Movement
        rb.velocity = new Vector3(xInput, yInput, 0);

        Vector2 lookDir = mousePos - rb.position;
        rb.rotation = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
    }

    public void Damage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Destroy(gameObject);
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
        animator.SetBool("Shoot", false);
    }

    void Reload()
    {
        currentAmmo = maxAmmo;
        canShoot = true;
    }
}
