using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Zombie : MonoBehaviour
{
    public Animator animator;

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
    float tempARDropChance;
    public float shotgunDropChance;
    float tempShotgunDropChance;
    public float minigunDropChance;

    [Header("Sound")]
    public AudioSource[] sounds;
    public float timeBetweenSounds;
    public float distanceToPlaySound;
    public bool canPlaySound;
    public float temp;
    bool alreadyRan;

    private void Start()
    {
        health = maxHealth;
        player = GameObject.Find("Player").GetComponent<Player>();
        tempARDropChance = ARDropChance;
        tempShotgunDropChance = tempARDropChance + shotgunDropChance;
        canPlaySound = true;
        alreadyRan = false;
        temp = timeBetweenSounds;
        if(((Vector2) player.transform.position - (Vector2) transform.position).magnitude <= distanceToPlaySound)
        {
            sounds[Random.Range(0, sounds.Length)].Play();
        }
    }

    private void Update() // Used for sound effects only
    {
        if(player == null)
        {
            return;
        }
        if (canPlaySound && ((Vector2) player.transform.position - (Vector2) transform.position).magnitude <= distanceToPlaySound)
        {
            sounds[Random.Range(0, sounds.Length)].Play();
            canPlaySound = false;
        }
        if (!canPlaySound && !alreadyRan)
        {
            InvokeRepeating("DecreaseSoundTime", 0f, .1f);
            alreadyRan = true;
        }
    }

    void DecreaseSoundTime()
    {
        if(temp > 0)
        {
            temp -= 0.1f;
            if(temp <= 0)
            {
                canPlaySound = true;
                alreadyRan = false;
                temp = timeBetweenSounds;
                CancelInvoke("DecreaseSoundTime");
            }
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            animator.SetBool("Hit", true);
            Invoke("ResetAnimation", 0.2f);
            Destroy(collision.gameObject);
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
            CancelInvoke("DamagePlayer");
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        ScoreboardTracker.damageDealt += damage;
        if (health <= 0 && !alreadyKilled)
        {
            Player.points += pointsGivenOnKill;
            ScoreboardTracker.score += pointsGivenOnKill;
            ScoreboardTracker.kills += 1;
            ZombieSpawning.zombiesKilledThisRound++;
            alreadyKilled = true;
            RandomDrop();
            Destroy(gameObject);
            ZombieSpawning.zombiePrefabInstances--;
            return;
        }
        Player.points += pointsGivenOnHit;
        ScoreboardTracker.score += pointsGivenOnHit;
    }

    public void RandomDrop()
    {
        if (Random.Range(0f, 1f) <= dropChance)
        {
            float thisRandom = Random.Range(0, ARDropChance + shotgunDropChance + minigunDropChance);
            Transform spawn = gameObject.transform;
            spawn.rotation = Quaternion.identity;
            if (thisRandom <= tempARDropChance)
            {
                Instantiate(assaultRifleDrop, spawn.position, spawn.rotation);
            } else if(thisRandom <= tempShotgunDropChance)
            {
                Instantiate(shotgunDrop, spawn.position, spawn.rotation);
            } else
            {
                Instantiate(minigunDrop, spawn.position, spawn.rotation);
            }
        }
    }

    void ResetAnimation()
    {
        animator.SetBool("Hit", false);
    }
}
