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
    Player player;

    [Header("Point Tracking")]
    public int pointsGivenOnHit = 1;
    public int pointsGivenOnKill = 3;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
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
            InvokeRepeating("DamagePlayer", 0f, 1f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isTouching = false;
        }
    }

    void DamagePlayer()
    {
        if(isTouching)
        {
            player.Damage(damage);
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
