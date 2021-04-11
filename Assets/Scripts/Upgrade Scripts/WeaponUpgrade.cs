using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public int firstUpgradePrice;
    public int secondUpgradePrice;
    public int thirdUpgradePrice;
    public float distance;

    public int firstUpgradeDamage;
    public int thirdUpgradeDamage;
    public int secondUpgradeAmmo;
    public int thirdUpgradeAmmo;

    public bool canBeBought;
    public int currentBuyableUpgrade;
    GameObject player;
    Player playerScript;
    Bullet bullet;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        bullet = GameObject.Find("Bullet").GetComponent<Bullet>();
        canBeBought = false;
        currentBuyableUpgrade = 1;
    }

    private void Update()
    {
        if ((player.transform.position - this.transform.position).magnitude < distance)
        {
            canBeBought = true;
            if(Input.GetKeyDown(KeyCode.E)) {
                if (currentBuyableUpgrade == 1 && Player.points >= firstUpgradePrice)
                {
                    Player.points -= firstUpgradePrice;
                    currentBuyableUpgrade = 2;
                    bullet.damage = firstUpgradeDamage;
                } else if(currentBuyableUpgrade == 2 && Player.points >= secondUpgradePrice)
                {
                    Player.points -= secondUpgradePrice;
                    currentBuyableUpgrade = 3;
                    playerScript.maxAmmo[0] = secondUpgradeAmmo;
                    if(playerScript.state == PlayerState.normal)
                    {
                        playerScript.currentAmmo = secondUpgradeAmmo;
                    }
                } else if(currentBuyableUpgrade == 3 && Player.points >= thirdUpgradePrice)
                {
                    Player.points -= firstUpgradePrice;
                    currentBuyableUpgrade = 4;
                    bullet.damage = thirdUpgradeDamage;
                    playerScript.maxAmmo[0] = thirdUpgradeAmmo;
                    if (playerScript.state == PlayerState.normal)
                    {
                        playerScript.currentAmmo = thirdUpgradeAmmo;
                    }
                } else
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
