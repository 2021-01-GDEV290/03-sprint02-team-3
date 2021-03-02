using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpgradeText : MonoBehaviour
{
    public GameObject speedUpgrade;
    int pointsNeeded;

    private void Start()
    {
        pointsNeeded = speedUpgrade.gameObject.GetComponent<SpeedUpgrade>().pointsNeeded;
    }

    private void Update()
    {
        if (speedUpgrade.GetComponent<SpeedUpgrade>().canBeBought)
        {
            this.GetComponent<Text>().text = "Press E to \nincrease speed\n(" + pointsNeeded + " points)";
        }
        else
        {
            this.GetComponent<Text>().text = "";
        }
        if (speedUpgrade.GetComponent<SpeedUpgrade>().bought) Destroy(this.gameObject);
    }
}
