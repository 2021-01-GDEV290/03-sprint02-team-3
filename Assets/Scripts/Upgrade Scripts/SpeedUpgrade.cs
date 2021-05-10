using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : MonoBehaviour
{
    public int firstUpgradePrice;
    public int secondUpgradePrice;
    public int thirdUpgradePrice;
    public float distance;

    public float firstUpgradeSpeed;
    public float thirdUpgradeSpeed;

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
                    playerScript.moveSpeed = firstUpgradeSpeed;
                }
                else if (currentBuyableUpgrade == 2 && Player.points >= secondUpgradePrice)
                {
                    Player.points -= secondUpgradePrice;
                    currentBuyableUpgrade++;
                    playerScript.dodge = true;
                }
                else if (currentBuyableUpgrade == 3 && Player.points >= thirdUpgradePrice)
                {
                    Player.points -= thirdUpgradePrice;
                    currentBuyableUpgrade++;
                    playerScript.moveSpeed = thirdUpgradeSpeed;
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
