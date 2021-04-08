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
        if (currentPoints != Player.points)
        {
            currentPoints = Player.points;
            UpdatePoints();
        }
    }

    void UpdatePoints()
    {
        txt.GetComponent<Text>();
        txt.text = "Points: " + currentPoints;
    }
}
