using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MysteryBoxText : MonoBehaviour
{
    public GameObject mysteryBox;
    int pointsNeeded;

    private void Start()
    {
        pointsNeeded = mysteryBox.gameObject.GetComponent<MysteryBox>().pointsNeeded;
    }

    private void Update()
    {
        if (mysteryBox.GetComponent<MysteryBox>().canBeBought)
        {
            GetComponent<Text>().text = "PRESS E TO \nOBTAIN A RANDOM WEAPON\n(" + pointsNeeded + " POINTS)";
        }
        else
        {
            GetComponent<Text>().text = "";
        }
    }
}
