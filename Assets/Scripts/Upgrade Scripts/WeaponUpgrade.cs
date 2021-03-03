using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgrade : MonoBehaviour
{
    public int firstUpgradePrice;
    public int secondUpgradePrice;
    public int thirdUpgradePrice;
    public int distance;

    public bool canBeBought;
    public int currentBuyableUpgrade;
    GameObject player;
    GameObject bullet;

    private void Start()
    {
        player = GameObject.Find("Player");
        bullet = GameObject.Find("Bullet");
        canBeBought = false;
        currentBuyableUpgrade = 1;
    }

    private void Update()
    {
        if ((player.transform.position - this.transform.position).magnitude < distance)
        {
            canBeBought = true;
            if(Input.GetKeyDown(KeyCode.E)) {
                if (currentBuyableUpgrade == 1 && PointsTracker.points >= firstUpgradePrice)
                {
                    PointsTracker.points -= firstUpgradePrice;
                    currentBuyableUpgrade = 2;
                    bullet.gameObject.GetComponent<DamageTracker>().damage = 2;
                } else if(currentBuyableUpgrade == 2 && PointsTracker.points >= secondUpgradePrice)
                {
                    PointsTracker.points -= secondUpgradePrice;
                    currentBuyableUpgrade = 3;
                    player.GetComponent<Shooting>().maxAmmo = 20;
                    player.GetComponent<Shooting>().currentAmmo = 20;
                } else if(currentBuyableUpgrade == 3 && PointsTracker.points >= thirdUpgradePrice)
                {
                    PointsTracker.points -= firstUpgradePrice;
                    currentBuyableUpgrade = 4;
                    bullet.gameObject.GetComponent<DamageTracker>().damage = 3;
                    player.GetComponent<Shooting>().maxAmmo = 30;
                    player.GetComponent<Shooting>().currentAmmo = 30;
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
