using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeText : MonoBehaviour
{
    public GameObject weaponUpgrade;

    int firstPointsNeeded;
    int secondPointsNeeded;
    int thirdPointsNeeded;

    private void Start()
    {
        firstPointsNeeded = weaponUpgrade.gameObject.GetComponent<WeaponUpgrade>().firstUpgradePrice;
        secondPointsNeeded = weaponUpgrade.gameObject.GetComponent<WeaponUpgrade>().secondUpgradePrice;
        thirdPointsNeeded = weaponUpgrade.gameObject.GetComponent<WeaponUpgrade>().thirdUpgradePrice;
    }

    private void Update()
    {
        if(weaponUpgrade.GetComponent<WeaponUpgrade>().currentBuyableUpgrade == 1)
        {
            if (weaponUpgrade.GetComponent<WeaponUpgrade>().canBeBought)
            {
                this.GetComponent<Text>().text = "PRESS E TO \nINCREASE DAMAGE\n(" + firstPointsNeeded + " POINTS)";
            } else
            {
                this.GetComponent<Text>().text = "";
            }
        } else if(weaponUpgrade.GetComponent<WeaponUpgrade>().currentBuyableUpgrade == 2)
        {
            if (weaponUpgrade.GetComponent<WeaponUpgrade>().canBeBought)
            {
                this.GetComponent<Text>().text = "PRESS E TO \nINCREASE AMMO\n(" + secondPointsNeeded + " POINTS)";
            } else
            {
                this.GetComponent<Text>().text = "";
            }
        } else if(weaponUpgrade.GetComponent<WeaponUpgrade>().currentBuyableUpgrade == 3)
        {
            if (weaponUpgrade.GetComponent<WeaponUpgrade>().canBeBought)
            {
                this.GetComponent<Text>().text = "PRESS TO E \nINCREASE DAMAGE & AMMO\n(" + thirdPointsNeeded + " POINTS)";
            } else
            {
                this.GetComponent<Text>().text = "";
            }
        } else
        {
            Destroy(this.gameObject);
        }
    }
}
