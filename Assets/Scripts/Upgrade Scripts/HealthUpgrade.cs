using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    public int firstUpgradePrice;
    public int secondUpgradePrice;
    public int thirdUpgradePrice;
    public float distance;

    public int firstUpgradeHealth;
    public float newRegenStartTime;
    public float newTimeBetweenRegens;

    public bool canBeBought;
    public int currentBuyableUpgrade;
    GameObject player;
    Player playerScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        canBeBought = false;
        currentBuyableUpgrade = 1;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        if ((player.transform.position - transform.position).magnitude < distance)
        {
            canBeBought = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentBuyableUpgrade == 1 && Player.points >= firstUpgradePrice)
                {
                    Player.points -= firstUpgradePrice;
                    currentBuyableUpgrade++;
                    playerScript.maxHealth = firstUpgradeHealth;
                    playerScript.health = firstUpgradeHealth;
                }
                else if (currentBuyableUpgrade == 2 && Player.points >= secondUpgradePrice)
                {
                    Player.points -= secondUpgradePrice;
                    currentBuyableUpgrade++;
                    playerScript.timeBeforeRegenStart = newRegenStartTime;
                    playerScript.timeBetweenRegens = newTimeBetweenRegens;
                }
                else if (currentBuyableUpgrade == 3 && Player.points >= thirdUpgradePrice)
                {
                    Player.points -= thirdUpgradePrice;
                    currentBuyableUpgrade++;
                    playerScript.damageZombiesOnTouch = true;
                }
                else 
                {
                    return;
                }
            }
        }
        else
        {
            canBeBought = false;
        }
    }
}
