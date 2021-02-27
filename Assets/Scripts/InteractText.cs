using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    public GameObject door;
    public string text;

    private void Start()
    {
        text = this.GetComponent<Text>().text;
    }

    private void Update()
    {
        if (door == null)
        {
            Destroy(this.gameObject);
        }
        if (door.GetComponent<Door>().canBeOpened)
        {
            this.GetComponent<Text>().text = text;
        } else
        {
            this.GetComponent<Text>().text = "";
        }
    }
}
