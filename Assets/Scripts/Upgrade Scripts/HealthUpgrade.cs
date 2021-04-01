using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    public int pointsNeeded;
    public int distance;
    public bool canBeBought;
    public bool bought;
    public int newHealth;

    GameObject player;
    Player playerScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        canBeBought = false;
        bought = false;
    }

    private void Update()
    {
        if ((player.transform.position - this.transform.position).magnitude < distance)
        {
            canBeBought = true;
            if (Input.GetKeyDown(KeyCode.E) && Player.points >= pointsNeeded && !bought)
            {
                Player.points -= pointsNeeded;
                bought = true;
                playerScript.maxHealth = newHealth;
                playerScript.health = newHealth;
            }
        }
        else
        {
            canBeBought = false;
        }
    }
}
