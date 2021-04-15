using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpgradeText : MonoBehaviour
{
    public GameObject healthUpgrade;
    int pointsNeeded;

    private void Start()
    {
        pointsNeeded = healthUpgrade.gameObject.GetComponent<HealthUpgrade>().pointsNeeded;
    }

    private void Update()
    {
        if (healthUpgrade.GetComponent<HealthUpgrade>().canBeBought)
        {
            this.GetComponent<Text>().text = "PRESS E TO \nINCREASE HEALTH\n(" + pointsNeeded + " POINTS)";
        }
        else
        {
            this.GetComponent<Text>().text = "";
        }
        if (healthUpgrade.GetComponent<HealthUpgrade>().bought) Destroy(this.gameObject);
    }
}
