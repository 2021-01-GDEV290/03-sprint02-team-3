using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoundText : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = "" + ZombieSpawning.currentRound;
    }
}
