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

    public bool dodge;
    public float dodgeChance;

    [Header("Health Tracker")]
    public int maxHealth;
    public int health;
    public float timeBeforeRegenStart;
    public float timeBetweenRegens;
    public float timeBetweenHits;

    public bool damageZombiesOnTouch;

    public bool canRegen;
    bool canBeHit;
    Collision2D currentCollision;

    [Header("Shooting")] // For all arrays, index 0 is pistol, 1 is AR, 2 is shotgun, and 3 is minigun
    public PlayerState state = PlayerState.normal;
    public Transform[] firePoint;
    public AudioSource[] shotSounds;
    public AudioSource[] reloadSounds;
    public AudioSource[] grunts;
    public GameObject bulletPrefab;
    public Animator animator;
    public float[] bulletForce;
    public int[] maxAmmo;
    public int currentAmmo;
    public int remainingAmmo; // For weapon drops
    public float[] reloadDelay;
    public float[] fireRate;
    public int numPelletsPerShotgunShot; // Number of pellets shot when the player is using the shotgun
    public int shotgunSpread; // How far the shotgun pellets spread when shot
    public int minigunSpread; // Shotgun spread, but for the minigun

    bool canShoot = true;
    bool reloading = false;

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
        dodge = false;

        // Shooting
        currentAmmo = maxAmmo[0];

        // Health
        health = maxHealth;
        canBeHit = true;
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
                if (Input.GetButton("Fire1") && canShoot && !reloading && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(0);
                    shotSounds[0].Play();
                }
                if ((Input.GetKeyDown(KeyCode.R) && !reloading && currentAmmo != maxAmmo[0]) || (currentAmmo <= 0 && !reloading))
                {
                    reloading = true;
                    canShoot = false;
                    reloadSounds[0].Play();
                    Invoke("Reload", reloadDelay[0]);
                }
                break;
            case PlayerState.assaultRifle:
                if (remainingAmmo == 0 && currentAmmo == 0)
                {
                    state = PlayerState.normal;
                    animator.SetInteger("Current State", 0);
                    currentAmmo = maxAmmo[0];
                    break;
                }
                if(Input.GetButton("Fire1") && canShoot && !reloading && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(1);
                    shotSounds[1].Play();
                }
                if ((Input.GetKeyDown(KeyCode.R) && !reloading && currentAmmo != maxAmmo[1]) || (currentAmmo <= 0 && !reloading && remainingAmmo > 0))
                {
                    reloading = true;
                    canShoot = false;
                    Invoke("Reload", reloadDelay[1]);
                    reloadSounds[1].Play();
                }
                break;
            case PlayerState.shotgun:
                if (remainingAmmo == 0 && currentAmmo == 0)
                {
                    state = PlayerState.normal;
                    animator.SetInteger("Current State", 0);
                    currentAmmo = maxAmmo[0];
                    break;
                }
                if (Input.GetButtonDown("Fire1") && canShoot && !reloading && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(2);
                    shotSounds[2].Play();
                }
                if ((Input.GetKeyDown(KeyCode.R) && !reloading && currentAmmo != maxAmmo[2]) || (currentAmmo <= 0 && !reloading && remainingAmmo > 0))
                {
                    reloading = true;
                    canShoot = false;
                    Invoke("Reload", reloadDelay[2]);
                    reloadSounds[2].Play();
                }
                break;
            case PlayerState.minigun:
                if (currentAmmo == 0)
                {
                    state = PlayerState.normal;
                    animator.SetInteger("Current State", 0);
                    currentAmmo = maxAmmo[0];
                    break;
                }
                if (Input.GetButton("Fire1") && canShoot && currentAmmo > 0 && !(PauseMenu.GameIsPaused))
                {
                    animator.SetBool("Shoot", true);
                    Shoot(3);
                    shotSounds[3].Play();
                }
                break;
        }

        // Health
        if (health < 0) health = 0;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentCollision = collision;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        currentCollision = null;
    }

    public void Damage(int damage)
    {
        if(!canBeHit)
        {
            return;
        }
        if(dodge)
        {
            if (Random.Range(0, 100) <= dodgeChance)
            {
                return;
            }
        }
        grunts[Random.Range(0, grunts.Length)].Play();
        canRegen = false;
        CancelInvoke("Regen");
        health = health - damage;
        if (damageZombiesOnTouch && currentCollision != null)
        {
            if(currentCollision.gameObject.tag == "Zombie" || currentCollision.gameObject.tag
                == "Fast Zombie" || currentCollision.gameObject.tag == "Boss Zombie")
            {
                currentCollision.gameObject.GetComponent<Zombie>().Damage(1);
            } else if(currentCollision.gameObject.tag == "Ranged Zombie")
            {
                currentCollision.gameObject.GetComponent<RangedZombie>().Damage(1);
            }
        }
        canBeHit = false;
        ScoreboardTracker.damageTaken += damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
        StartCoroutine("WaitUntilNextHit");
    }

    IEnumerator WaitUntilNextHit()
    {
        yield return new WaitForSeconds(timeBetweenHits);
        canBeHit = true;
    }

    void Shoot(int index)
    {
        if (state == PlayerState.shotgun)
        {
            for (int i = 0; i < numPelletsPerShotgunShot; i++)
            {
                GameObject bullet = Instantiate(bulletPrefab, firePoint[index].position,
                    firePoint[index].rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                float spreadAngle = Random.Range(-shotgunSpread, shotgunSpread) + shotgunSpread;
                var x = firePoint[index].position.x - transform.position.x;
                var y = firePoint[index].position.y - transform.position.y;
                float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
                var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad),
                    Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
                rb.AddForce(((Vector2)firePoint[index].up + MovementDirection) * bulletForce[index], ForceMode2D.Impulse);
            }
        } else if (state == PlayerState.minigun)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint[index].position,
                    firePoint[index].rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            float spreadAngle = Random.Range(-minigunSpread, minigunSpread) + minigunSpread;
            var x = firePoint[index].position.x - transform.position.x;
            var y = firePoint[index].position.y - transform.position.y;
            float rotateAngle = spreadAngle + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);
            var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad),
                Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
            rb.AddForce(((Vector2)firePoint[index].up + MovementDirection) * bulletForce[index], ForceMode2D.Impulse);
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
        int ammoNeeded;
        switch (state)
        {
            case PlayerState.normal:
                currentAmmo = maxAmmo[0];
                break;
            case PlayerState.assaultRifle:
                ammoNeeded = maxAmmo[1] - currentAmmo;
                if(remainingAmmo >= ammoNeeded)
                {
                    currentAmmo += ammoNeeded;
                    remainingAmmo -= ammoNeeded;
                } else
                {
                    currentAmmo += remainingAmmo;
                    remainingAmmo = 0;
                }
                break;
            case PlayerState.shotgun:
                ammoNeeded = maxAmmo[2] - currentAmmo;
                if (remainingAmmo >= ammoNeeded)
                {
                    currentAmmo += ammoNeeded;
                    remainingAmmo -= ammoNeeded;
                }
                else
                {
                    currentAmmo += remainingAmmo;
                    remainingAmmo = 0;
                }
                break;
        }
        canShoot = true;
        reloading = false;
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
