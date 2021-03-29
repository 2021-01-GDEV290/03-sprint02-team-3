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
        currentHealth = this.gameObject.GetComponent<Player>().maxHealth;
    }

    private void Update()
    {
        if(player.GetComponent<Player>().maxHealth == currentHealth)
        {
            canRegen = false;
        }
        if(player.GetComponent<Player>().health != currentHealth)
        {
            canRegen = false;
            currentHealth = player.GetComponent<Player>().health;
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
        int tempHealth = player.GetComponent<Player>().health;
        int tempMaxHealth = player.GetComponent<Player>().maxHealth;
        if(tempHealth == tempMaxHealth)
        {
            return;
        }
        if(canRegen)
        {
            player.GetComponent<Player>().health++;
        }
    }
}
