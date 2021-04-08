using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableSpawn : MonoBehaviour
{
    public GameObject correspondingArea;
    public bool isAvailable = false;
    public bool added = false;

    AreaEnterDetection area;

    private void Start()
    {
        area = correspondingArea.GetComponent<AreaEnterDetection>();
    }

    private void Update()
    {
        if(area.isOpen)
        {
            isAvailable = true;
        }
    }
}
