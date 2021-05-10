using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpgradeText : MonoBehaviour
{
    public GameObject speedUpgrade;
    SpeedUpgrade script;

    private void Start()
    {
        script = speedUpgrade.GetComponent<SpeedUpgrade>();
    }

    private void Update()
    {
        if (script.currentBuyableUpgrade == 1)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE SPEED\n(" + 
                    script.firstUpgradePrice + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        }
        else if (script.currentBuyableUpgrade == 2)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E FOR A \nCHANCE TO TAKE NO DAMAGE ON A HIT\n(" + 
                    script.secondUpgradePrice + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        }
        else if (script.currentBuyableUpgrade == 3)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE SPEED\n(" 
                    + script.thirdUpgradePrice + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
