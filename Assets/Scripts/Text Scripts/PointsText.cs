using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsText : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text txt;

    int currentPoints;

    private void Update()
    {
        txt.GetComponent<Text>();
        txt.text = "POINTS: " + Player.points;
    }
}
