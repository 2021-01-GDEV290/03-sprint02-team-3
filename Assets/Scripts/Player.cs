using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    normal,
    assaultRifle,
    shotgun,
    minigun
}

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
    public float timeBeforeRegenStart;
    public float timeBetweenRegens;

    bool canRegen;

    [Header("Shooting")] // For all arrays, index 0 is normal, 1 is AR, 2 is shotgun, and 3 is minigun
    public PlayerState state = PlayerState.normal;
    public Transform[] firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public float[] bulletForce;
    public int[] maxAmmo;
    public int currentAmmo;
    public float[] reloadDelay;
    public float[] fireRate;
    public int numPelletsPerShot; // For shotgun only

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
        currentAmmo = maxAmmo[0];

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
        switch (state)
        {
            case PlayerState.normal:
                if (Input.GetButtonDown("Fire1") && canShoot && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(0);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    canShoot = false;
                    Invoke("Reload", fireRate[0]);
                }
                break;
            case PlayerState.assaultRifle:
                if(Input.GetButton("Fire1") && canShoot && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(1);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    canShoot = false;
                    Invoke("Reload", fireRate[1]);
                }
                break;
            case PlayerState.shotgun:
                if (Input.GetButtonDown("Fire1") && canShoot && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(2);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    canShoot = false;
                    Invoke("Reload", fireRate[2]);
                }
                break;
            case PlayerState.minigun:
                if (Input.GetButton("Fire1") && canShoot && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(3);
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    canShoot = false;
                    Invoke("Reload", fireRate[3]);
                }
                break;
        }

        // Health
        if (health != maxHealth && !canRegen)
        {
            canRegen = true;
            InvokeRepeating("Regen", timeBeforeRegenStart, timeBetweenRegens);
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
        canRegen = false;
        CancelInvoke("Regen");
        health = health - damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Shoot(int index)
    {
        if (state == PlayerState.shotgun)
        {
            for (int i = 0; i < numPelletsPerShot; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint[index].position, 
                    firePoint[index].rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                float spreadAngle = Random.Range(-30, 30) + 30;
                var x = firePoint[index].position.x - transform.position.x;
                var y = firePoint[index].position.y - transform.position.y;
                float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
                var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad),
                    Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
                rb.AddForce(((Vector2) firePoint[index].up + MovementDirection) * bulletForce[index], ForceMode2D.Impulse);
            }
        } else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint[index].position, firePoint[index].rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint[index].up * bulletForce[index], ForceMode2D.Impulse);
        }
        currentAmmo--;
        canShoot = false;
        Invoke("TimeBeforeNextShoot", fireRate[index]);
    }

    void TimeBeforeNextShoot()
    {
        canShoot = true;
        animator.SetBool("Shoot", false);
    }

    void Reload()
    {
        switch (state)
        {
            case PlayerState.normal:
                currentAmmo = maxAmmo[0];
                break;
            case PlayerState.assaultRifle:
                currentAmmo = maxAmmo[1];
                break;
            case PlayerState.shotgun:
                currentAmmo = maxAmmo[2];
                break;
            case PlayerState.minigun:
                currentAmmo = maxAmmo[3];
                break;
        }
        canShoot = true;
    }

    void Regen()
    {
        if (canRegen)
        {
            health++;
            if (health == maxHealth)
            {
                canRegen = false;
                CancelInvoke("Regen");
            }
        }
        else
        {
            CancelInvoke("Regen");
        }
    }
}
