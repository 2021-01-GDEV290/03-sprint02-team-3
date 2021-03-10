using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawning : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject zombiePrefab;

    bool canSpawn;
    private readonly List<Transform> availableSpawns = new List<Transform>();

    private void Start()
    {
        canSpawn = true;
    }
    private void Update()
    {
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
            Invoke("SpawnZombie", 3f);
            canSpawn = false;
        }
    }

    void SpawnZombie()
    {
        Transform randomSpawn = availableSpawns[Random.Range(0, availableSpawns.Count)];
        Instantiate(zombiePrefab, randomSpawn.position, randomSpawn.rotation);
        canSpawn = true;
    }
}
