using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgradeText : MonoBehaviour
{
    public GameObject healthUpgrade;

    int firstPointsNeeded;
    int secondPointsNeeded;
    int thirdPointsNeeded;

    private void Start()
    {
        firstPointsNeeded = healthUpgrade.gameObject.GetComponent<HealthUpgrade>().firstUpgradePrice;
        secondPointsNeeded = healthUpgrade.gameObject.GetComponent<HealthUpgrade>().secondUpgradePrice;
        thirdPointsNeeded = healthUpgrade.gameObject.GetComponent<HealthUpgrade>().thirdUpgradePrice;
    }

    private void Update()
    {
        if (healthUpgrade.GetComponent<HealthUpgrade>().currentBuyableUpgrade == 1)
        {
            if (healthUpgrade.GetComponent<HealthUpgrade>().canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE HEALTH\n(" + firstPointsNeeded + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        } else if(healthUpgrade.GetComponent<HealthUpgrade>().currentBuyableUpgrade == 2)
        {
            if (healthUpgrade.GetComponent<HealthUpgrade>().canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE HEALTH REGENERATION\n(" + secondPointsNeeded + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        } else if(healthUpgrade.GetComponent<HealthUpgrade>().currentBuyableUpgrade == 3)
        {
            if (healthUpgrade.GetComponent<HealthUpgrade>().canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nDAMAGE ZOMBIES AFTER THEY DAMAGE YOU\n(" + thirdPointsNeeded + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        } else
        {
            Destroy(gameObject);
        }
    }
}
