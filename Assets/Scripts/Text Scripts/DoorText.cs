﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorText : MonoBehaviour
{
    public GameObject door;
    public int pointsNeeded;

    private void Start()
    {
        pointsNeeded = door.gameObject.GetComponent<Door>().pointsNeeded;
    }

    private void Update()
    {
        if (!door.gameObject.activeSelf)
        {
            Destroy(this.gameObject);
        }
        if (door.GetComponent<Door>().canBeOpened)
        {
            this.GetComponent<Text>().text = "PRESS E\nTO OPEN\n(" + pointsNeeded + " POINTS)";
        } else
        {
            this.GetComponent<Text>().text = "";
        }
    }
}
