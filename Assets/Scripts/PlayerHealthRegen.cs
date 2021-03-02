using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthRegen : MonoBehaviour
{
    GameObject player;
    bool canRegen;
    int currentHealth;

    private void Start()
    {
        player = this.gameObject;
        canRegen = false;
        currentHealth = this.gameObject.GetComponent<HealthTracker>().maxHealth;
    }

    private void Update()
    {
        if(player.GetComponent<HealthTracker>().maxHealth == currentHealth)
        {
            canRegen = false;
        }
        if(player.GetComponent<HealthTracker>().health != currentHealth)
        {
            canRegen = false;
            currentHealth = player.GetComponent<HealthTracker>().health;
            Invoke("RegenDelay", 3f);
        }
        if(canRegen)
        {
            Invoke("HealthRegen", 0.5f);
        }
    }

    void RegenDelay ()
    {
        canRegen = true;
    }

    void HealthRegen()
    {
        HealthTracker temp = player.GetComponent<HealthTracker>();
        if(temp.health == temp.maxHealth)
        {
            return;
        }
        if(canRegen)
        {
            player.GetComponent<HealthTracker>().health++;
        }
    }
}
