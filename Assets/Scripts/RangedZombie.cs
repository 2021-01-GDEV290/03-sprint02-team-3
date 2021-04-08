using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedZombie : Zombie
{
    [Header("Ranged Zombie")]
    public float startTime; // How long it takes until the ranged zombie shoots it's first projectile
    public float repeatTime; // How often the ranged zombie shoots projectiles
    public GameObject projectile;
    public Transform firePoint;
    public float projectileForce;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
        InvokeRepeating("RangedAttack", startTime, repeatTime);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;
        }
        if(!isTouching)
        {
            CancelInvoke();
            InvokeRepeating("RangedAttack", repeatTime, repeatTime);
        }
    }

    void RangedAttack()
    {
        if(!isTouching)
        {
            GameObject proj = Instantiate(projectile, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
        }
    }
}
