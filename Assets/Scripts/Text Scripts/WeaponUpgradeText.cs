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
    int fourthPlusPointsNeeded;

    private void Start()
    {
        firstPointsNeeded = weaponUpgrade.gameObject.GetComponent<WeaponUpgrade>().firstUpgradePrice;
        secondPointsNeeded = weaponUpgrade.gameObject.GetComponent<WeaponUpgrade>().secondUpgradePrice;
        thirdPointsNeeded = weaponUpgrade.gameObject.GetComponent<WeaponUpgrade>().thirdUpgradePrice;
        fourthPlusPointsNeeded = weaponUpgrade.gameObject.GetComponent<WeaponUpgrade>().fourthPlusUpgradePrice;
    }

    private void Update()
    {
        if(weaponUpgrade.GetComponent<WeaponUpgrade>().currentBuyableUpgrade == 1)
        {
            if (weaponUpgrade.GetComponent<WeaponUpgrade>().canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE DAMAGE\n(" + firstPointsNeeded + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        } else if(weaponUpgrade.GetComponent<WeaponUpgrade>().currentBuyableUpgrade == 2)
        {
            if (weaponUpgrade.GetComponent<WeaponUpgrade>().canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE AMMO\n(" + secondPointsNeeded + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        } else if(weaponUpgrade.GetComponent<WeaponUpgrade>().currentBuyableUpgrade == 3)
        {
            if (weaponUpgrade.GetComponent<WeaponUpgrade>().canBeBought)
            {
                GetComponent<Text>().text = "PRESS TO E \nINCREASE DAMAGE & AMMO\n(" + thirdPointsNeeded + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        } else
        {
            if (weaponUpgrade.GetComponent<WeaponUpgrade>().canBeBought)
            {
                GetComponent<Text>().text = "PRESS TO E \nINCREASE DAMAGE\n(" + fourthPlusPointsNeeded + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        }
    }
}
