using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public int firstUpgradePrice;
    public int secondUpgradePrice;
    public int thirdUpgradePrice;
    public int fourthPlusUpgradePrice;
    public int priceIncreaser;
    public float distance;

    public int firstUpgradeDamage;
    public int thirdUpgradeDamage;
    public int secondUpgradeAmmo;
    public int secondUpgradeMinigunAmmo;
    public int thirdUpgradeAmmo;
    public int thirdUpgradeMinigunAmmo;

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
        fourthPlusUpgradePrice = thirdUpgradePrice + priceIncreaser;
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
            if(Input.GetKeyDown(KeyCode.E)) {
                if (currentBuyableUpgrade == 1 && Player.points >= firstUpgradePrice)
                {
                    Player.points -= firstUpgradePrice;
                    currentBuyableUpgrade++;
                    bullet.damage = firstUpgradeDamage;
                } else if(currentBuyableUpgrade == 2 && Player.points >= secondUpgradePrice)
                {
                    Player.points -= secondUpgradePrice;
                    currentBuyableUpgrade++;
                    playerScript.maxAmmo[0] = secondUpgradeAmmo;
                    WeaponDrop.ARStockAmmoMultiplier++;
                    WeaponDrop.shotgunStockAmmoMultiplier++;
                    playerScript.maxAmmo[3] = secondUpgradeMinigunAmmo;
                    switch (playerScript.state)
                    {
                        case PlayerState.normal:
                            playerScript.currentAmmo = secondUpgradeAmmo;
                            break;
                        case PlayerState.assaultRifle:
                            playerScript.currentAmmo = playerScript.maxAmmo[1];
                            playerScript.remainingAmmo = playerScript.maxAmmo[1] * WeaponDrop.ARStockAmmoMultiplier;
                            break;
                        case PlayerState.shotgun:
                            playerScript.currentAmmo = playerScript.maxAmmo[2];
                            playerScript.remainingAmmo = playerScript.maxAmmo[2] * WeaponDrop.shotgunStockAmmoMultiplier;
                            break;
                        case PlayerState.minigun:
                            playerScript.currentAmmo = playerScript.maxAmmo[3];
                            break;
                    }
                } else if(currentBuyableUpgrade == 3 && Player.points >= thirdUpgradePrice)
                {
                    Player.points -= thirdUpgradePrice;
                    currentBuyableUpgrade = 4;
                    bullet.damage = thirdUpgradeDamage;
                    playerScript.maxAmmo[0] = thirdUpgradeAmmo;
                    WeaponDrop.ARStockAmmoMultiplier++;
                    WeaponDrop.shotgunStockAmmoMultiplier++;
                    playerScript.maxAmmo[3] = thirdUpgradeMinigunAmmo;
                    switch (playerScript.state)
                    {
                        case PlayerState.normal:
                            playerScript.currentAmmo = secondUpgradeAmmo;
                            break;
                        case PlayerState.assaultRifle:
                            playerScript.currentAmmo = playerScript.maxAmmo[1];
                            playerScript.remainingAmmo = playerScript.maxAmmo[1] * WeaponDrop.ARStockAmmoMultiplier;
                            break;
                        case PlayerState.shotgun:
                            playerScript.currentAmmo = playerScript.maxAmmo[2];
                            playerScript.remainingAmmo = playerScript.maxAmmo[2] * WeaponDrop.shotgunStockAmmoMultiplier;
                            break;
                        case PlayerState.minigun:
                            playerScript.currentAmmo = playerScript.maxAmmo[3];
                            break;
                    }
                } else if(currentBuyableUpgrade > 3 && Player.points >= fourthPlusUpgradePrice)
                {
                    Player.points -= fourthPlusUpgradePrice;
                    currentBuyableUpgrade++;
                    bullet.damage++;
                    fourthPlusUpgradePrice += priceIncreaser;
                }
            }
        }
        else
        {
            canBeBought = false;
        }
    }
}
