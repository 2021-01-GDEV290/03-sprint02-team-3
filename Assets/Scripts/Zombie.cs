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
    public string[] tags;
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
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.gameObject.tag == tags[i])
            {
                isTouching = true;
                col = collision;
                InvokeRepeating("DamagePlayer", 0f, 1f);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (collision.gameObject.tag == tags[i])
            {
                isTouching = false;
                col = null;
            }
        }
    }

    void DamagePlayer()
    {
        if(isTouching)
        {
            col.gameObject.GetComponent<HealthTracker>().Damage(damage);
        }
    }

    public void Damage(int damage)
    {
        health = health - damage;
        if (health <= 0)
        {
            PointsTracker.points += pointsGivenOnKill;
            ZombieSpawning.zombiesKilledThisRound++;
            Destroy(this.gameObject);
            return;
        }
        PointsTracker.points += pointsGivenOnHit;
    }
}
