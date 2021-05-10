using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUpgradeText : MonoBehaviour
{
    public GameObject weaponUpgrade;
    WeaponUpgrade script;

    private void Start()
    {
        script = weaponUpgrade.GetComponent<WeaponUpgrade>();
    }

    private void Update()
    {
        if(script.currentBuyableUpgrade == 1)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE DAMAGE\n(" + 
                    script.firstUpgradePrice + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        } else if(script.currentBuyableUpgrade == 2)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE AMMO\n(" +
                    script.secondUpgradePrice + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        } else if(script.currentBuyableUpgrade == 3)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS TO E \nINCREASE DAMAGE & AMMO\n(" +
                    script.thirdUpgradePrice + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        } else
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS TO E \nINCREASE DAMAGE\n(" +
                    script.fourthPlusUpgradePrice + " POINTS)";
            } else
            {
                GetComponent<Text>().text = "";
            }
        }
    }
}
