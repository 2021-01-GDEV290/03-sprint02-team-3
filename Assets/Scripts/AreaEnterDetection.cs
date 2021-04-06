using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnterDetection : MonoBehaviour
{
    public string areaName;
    public bool isOpen;
    public GameObject[] correspondingDoors;

    private void Start()
    {
        isOpen = false;
    }
    private void Update()
    {
        if(correspondingDoors.Length == 0)
        {
            isOpen = true;
            return;
        }
        for(int i = 0; i < correspondingDoors.Length; i++)
        {
            if(!correspondingDoors[i].gameObject.activeSelf)
            {
                isOpen = true;
            }
        }
    }
}
