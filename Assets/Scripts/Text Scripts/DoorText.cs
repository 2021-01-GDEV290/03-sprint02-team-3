using System.Collections;
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
        if (door == null)
        {
            Destroy(this.gameObject);
        }
        if (door.GetComponent<Door>().canBeOpened)
        {
            this.GetComponent<Text>().text = "Press E\nto open\n(" + pointsNeeded + " points)";
        } else
        {
            this.GetComponent<Text>().text = "";
        }
    }
}
