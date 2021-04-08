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
    bool alreadyKilled = false;

    [Header("Damage Tracking")]
    public int damage = 1;
    public bool isTouching = false;
    public Player player;

    [Header("Point Tracking")]
    public int pointsGivenOnHit = 1;
    public int pointsGivenOnKill = 3;

    [Header("Weapon Drop")]
    public GameObject assaultRifleDrop;
    public GameObject shotgunDrop;
    public GameObject minigunDrop;
    public float dropChance; // Does a weapon drop if the zombie dies?
    public float ARDropChance;
    public float shotgunDropChance;
    public float minigunDropChance;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
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
        if (health <= 0 && !alreadyKilled)
        {
            Player.points += pointsGivenOnKill;
            ZombieSpawning.zombiesKilledThisRound++;
            alreadyKilled = true;
            RandomDrop();
            Destroy(gameObject);
            return;
        }
        Player.points += pointsGivenOnHit;
    }

    public void RandomDrop()
    {
        if (Random.Range(0f, 1f) <= dropChance)
        {
            float thisRandom = Random.Range(0, ARDropChance + shotgunDropChance + minigunDropChance);
            Transform spawn = gameObject.transform;
            spawn.rotation = Quaternion.identity;
            if (thisRandom <= ARDropChance)
            {
                Instantiate(assaultRifleDrop, spawn.position, spawn.rotation);
            } else if(thisRandom > ARDropChance && thisRandom <= shotgunDropChance)
            {
                Instantiate(shotgunDrop, spawn.position, spawn.rotation);
            } else
            {
                Instantiate(minigunDrop, spawn.position, spawn.rotation);
            }
        }
    }
}
