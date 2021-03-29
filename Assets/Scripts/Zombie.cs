using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Zombie : MonoBehaviour
{
    public AIPath path;

    [Header("Health Tracking")]
    public int maxHealth = 3;
    public int health = 3;

    [Header("Damage Tracking")]
    public int damage = 1;
    public bool isTouching = false;
    Collision2D col;

    [Header("Point Tracking")]
    public int pointsGivenOnHit = 1;
    public int pointsGivenOnKill = 3;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameObject.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            isTouching = true;
            col = collision;
            InvokeRepeating("DamagePlayer", 0f, 1f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;
            col = null;
        }
    }

    void DamagePlayer()
    {
        if(isTouching)
        {
            col.gameObject.GetComponent<Player>().Damage(damage);
        } else
        {
            CancelInvoke();
        }
    }

    public void Damage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            Player.points += pointsGivenOnKill;
            ZombieSpawning.zombiesKilledThisRound++;
            Destroy(this.gameObject);
            return;
        }
        Player.points += pointsGivenOnHit;
    }
}
