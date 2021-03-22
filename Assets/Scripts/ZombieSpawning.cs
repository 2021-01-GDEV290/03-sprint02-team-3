using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawning : MonoBehaviour
{
    [Header("For Show Only")]
    public static int currentRound = 1; // The current round the player is on
    public static int numberOfZombiesRoundOne = 5; // The number of zombies that can spawn in the first round
    //                                         (changing this changes amount of zombies per round)
    
    public static int maxZombieSpawnsThisRound; // The number of zombies that can spawn on this round
    //                                      (this is determined by the first round spawns)

    public static int zombiesSpawnedThisRound = 0; // The amount of zombies spawned this round
    public static int zombiesKilledThisRound = 0; // The amount of zombies killed this round
    public int zombieKillsTracked; // So I can track the above variable to make sure it is working as intended

    public Transform[] spawnPoints; // An array of all spawn points
    public GameObject zombiePrefab; // The zombie prefab
    public float nextRoundDelay; // Determines how long to wait before starting the next round
    public float delayBetweenSpawns; // Determines how long to wait before another zombie is spawned

    bool canSpawn;
    bool roundDelayBegun = false;
    List<Transform> availableSpawns = new List<Transform>();

    private void Start()
    {
        canSpawn = true;
        maxZombieSpawnsThisRound = numberOfZombiesRoundOne;
    }
    private void Update()
    {
        zombieKillsTracked = zombiesKilledThisRound;

        if(zombiesKilledThisRound >= maxZombieSpawnsThisRound && !roundDelayBegun)
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

        if(canSpawn)
        {
            Invoke("SpawnZombie", delayBetweenSpawns);
            canSpawn = false;
        }
    }

    void SpawnZombie()
    {
        Transform randomSpawn = availableSpawns[Random.Range(0, availableSpawns.Count)];
        Instantiate(zombiePrefab, randomSpawn.position, randomSpawn.rotation);
        zombiesSpawnedThisRound++;
        canSpawn = true;
    }

    void StartNextRound()
    {
        currentRound++;
        maxZombieSpawnsThisRound = maxZombieSpawnsThisRound + 5;
        zombiesSpawnedThisRound = 0;
        zombiesKilledThisRound = 0;
        roundDelayBegun = false;
    }

    public static void ResetGame()
    {
        currentRound = 1;
        numberOfZombiesRoundOne = 5;
        zombiesSpawnedThisRound = 0;
        zombiesKilledThisRound = 0;
    }
}
