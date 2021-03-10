using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableSpawn : MonoBehaviour
{
    public GameObject correspondingArea;
    public bool isAvailable = false;
    public bool added = false;

    private void Update()
    {
        if(correspondingArea.GetComponent<AreaEnterDetection>().isOpen)
        {
            isAvailable = true;
        }
    }
}
