using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgradeText : MonoBehaviour
{
    public GameObject healthUpgrade;
    HealthUpgrade script;

    private void Start()
    {
        script = healthUpgrade.GetComponent<HealthUpgrade>();
    }

    private void Update()
    {
        if (script.currentBuyableUpgrade == 1)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE HEALTH\n(" + 
                    script.firstUpgradePrice + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        } else if(script.currentBuyableUpgrade == 2)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nINCREASE HEALTH REGENERATION\n(" 
                    + script.secondUpgradePrice + " POINTS)";
            }
            else
            {
                GetComponent<Text>().text = "";
            }
        } else if(script.currentBuyableUpgrade == 3)
        {
            if (script.canBeBought)
            {
                GetComponent<Text>().text = "PRESS E TO \nDAMAGE ZOMBIES AFTER THEY DAMAGE YOU\n(" 
                    + script.thirdUpgradePrice + " POINTS)";
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
