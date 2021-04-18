using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawning : MonoBehaviour
{
    [Header("For Show Only")]
    public static int currentRound = 1; // The current round the player is on
    private static int numberOfZombiesRoundOne; // The number of zombies that can spawn in the first round
    public int numZombiesRoundOne = 5;
    private static int numberOfZombiesIncrease; // The number to increase max spawns by each round
    public int numZombiesIncrease = 3;
    public int increaser = 1; // The number to increase numberOfZombiesIncrease by each round
    public static int maxZombieSpawnsThisRound; // The number of zombies that can spawn on this round

    public int firstBossRoundNumber; // This is a random number from n-1 to n+1 (so if it is 5, the first boss
    //                            spawns on a random round from 4-6)
    public int nextBossRoundNumber; // Picks a random number from n-1 to n+1 to be the round for the next boss round
    int nextBossRound; // The next boss round

    public static int zombiesSpawnedThisRound = 0; // The amount of zombies spawned this round
    public int zombiesSpawnedOnBossRoundPlusBoss = 11; // The number of zombies spawned on any given boss round
    public static int zombiesKilledThisRound = 0; // The amount of zombies killed this round
    public int zombieKillsTracked; // So I can track the above variable to make sure it is working as intended

    public Transform[] spawnPoints; // An array of all spawn points
    public GameObject[] zombiePrefabs; // The zombie prefab
    public float normalZombieSpawnChance;
    float tempNormalZombieSpawnChance;
    public float fastZombieSpawnChance;
    float tempFastZombieSpawnChance;
    public float rangedZombieSpawnChance;
    public float nextRoundDelay; // Determines how long to wait before starting the next round
    public float delayBetweenSpawns; // Determines how long to wait before another zombie is spawned

    int currentRoundHealth;
    public int zombieHealthIncreaser;
    int currentRoundBossHealth;
    public int bossHealthIncreaser;
    bool alreadyRan;

    bool isBossRound;
    bool canSpawn;
    bool roundDelayBegun = false;
    List<Transform> availableSpawns = new List<Transform>();

    private void Start()
    {
        numberOfZombiesRoundOne = numZombiesRoundOne;
        numberOfZombiesIncrease = numZombiesIncrease;
        canSpawn = true;
        maxZombieSpawnsThisRound = numberOfZombiesRoundOne;
        tempNormalZombieSpawnChance = normalZombieSpawnChance;
        tempFastZombieSpawnChance = tempNormalZombieSpawnChance + fastZombieSpawnChance;
        nextBossRound = Random.Range(firstBossRoundNumber - 1, firstBossRoundNumber + 1);
        Debug.Log("First boss round is round " + nextBossRound);
        isBossRound = false;
        currentRoundHealth = 0;
        alreadyRan = false;
    }
    private void Update()
    {
        zombieKillsTracked = zombiesKilledThisRound;

        if (zombiesKilledThisRound >= maxZombieSpawnsThisRound && isBossRound && !alreadyRan) // The boss round has ended
        {
            currentRoundHealth += zombieHealthIncreaser;
            currentRoundBossHealth += bossHealthIncreaser;
            alreadyRan = true;
        }

        if (zombiesKilledThisRound >= maxZombieSpawnsThisRound && !roundDelayBegun)
        {
            Invoke("StartNextRound", nextRoundDelay);
            roundDelayBegun = true;
        }

        if(zombiesSpawnedThisRound >= maxZombieSpawnsThisRound)
        {
            return;
        }

        for(int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i].GetComponent<AvailableSpawn>().isAvailable &&
                (!spawnPoints[i].GetComponent<AvailableSpawn>().added))
            {
                availableSpawns.Add(spawnPoints[i]);
                spawnPoints[i].GetComponent<AvailableSpawn>().added = true;
            }
        }

        if(canSpawn && !isBossRound)
        {
            Invoke("SpawnZombie", delayBetweenSpawns);
            canSpawn = false;
        }

        if(canSpawn && isBossRound)
        {
            Invoke("SpawnZombieBossRound", delayBetweenSpawns);
            canSpawn = false;
        }
    }

    void SpawnZombie()
    {
        Transform randomSpawn = availableSpawns[Random.Range(0, availableSpawns.Count)];
        float thisRandom = Random.Range(0, normalZombieSpawnChance + fastZombieSpawnChance
            + rangedZombieSpawnChance);
        GameObject zombie;
        if (thisRandom <= tempNormalZombieSpawnChance)
        {
            zombie = Instantiate(zombiePrefabs[0], randomSpawn.position + new Vector3(0, 0, -10),
                randomSpawn.rotation);
        }
        else if (thisRandom <= tempFastZombieSpawnChance)
        {
            zombie = Instantiate(zombiePrefabs[1], randomSpawn.position + new Vector3(0, 0, -10),
                randomSpawn.rotation);
        }
        else
        {
            zombie = Instantiate(zombiePrefabs[2], randomSpawn.position + new Vector3(0, 0, -10),
                randomSpawn.rotation);
        }
        Zombie zombieScript = zombie.GetComponent<Zombie>();
        zombieScript.maxHealth += currentRoundHealth;
        zombieScript.health += currentRoundHealth;
        Debug.Log("Spawned zombie with the number " + thisRandom);
        zombiesSpawnedThisRound++;
        canSpawn = true;
    }

    void SpawnZombieBossRound()
    {
        Transform randomSpawn = availableSpawns[Random.Range(0, availableSpawns.Count)];
        GameObject zombie;
        Zombie zombieScript;
        if (zombiesSpawnedThisRound == 0)
        {
            zombie = Instantiate(zombiePrefabs[3], randomSpawn.position + new Vector3(0, 0, -10), 
                randomSpawn.rotation);
            zombieScript = zombie.GetComponent<Zombie>();
            zombieScript.maxHealth += currentRoundBossHealth;
            zombieScript.health += currentRoundBossHealth;
        } else
        {
            zombie = Instantiate(zombiePrefabs[1], randomSpawn.position + new Vector3(0, 0, -10)
                , randomSpawn.rotation);
            zombieScript = zombie.GetComponent<Zombie>();
            zombieScript.maxHealth += currentRoundHealth;
            zombieScript.health += currentRoundHealth;
        }
        zombiesSpawnedThisRound++;
        canSpawn = true;
    }

    void StartNextRound()
    {
        if(isBossRound)
        {
            isBossRound = false;
            alreadyRan = false;
        }
        currentRound++;
        zombiesSpawnedThisRound = 0;
        zombiesKilledThisRound = 0;
        roundDelayBegun = false;
        if (nextBossRound == currentRound)
        {
            StartBossRound();
            return;
        }
        maxZombieSpawnsThisRound += numberOfZombiesIncrease;
        numberOfZombiesIncrease += increaser;
    }
    
    void StartBossRound()
    {
        isBossRound = true;
        maxZombieSpawnsThisRound = zombiesSpawnedOnBossRoundPlusBoss;
        nextBossRound += Random.Range(nextBossRoundNumber - 1, nextBossRoundNumber + 1);
        Debug.Log("Next boss round is round " + nextBossRound);
    }

    public static void ResetGame()
    {
        currentRound = 1;
        numberOfZombiesRoundOne = 5;
        zombiesSpawnedThisRound = 0;
        zombiesKilledThisRound = 0;
        numberOfZombiesIncrease = 3;
    }
}
