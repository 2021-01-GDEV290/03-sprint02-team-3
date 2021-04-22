using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    public AudioSource sound;

    public float distance;

    GameObject player;
    bool currentlyPaused;

    private void Start()
    {
        player = GameObject.Find("Player");
        currentlyPaused = true;
    }

    private void Update()
    {
        Debug.Log((player.transform.position - transform.position).magnitude);
        if(currentlyPaused)
        {
            if((player.transform.position - transform.position).magnitude <= distance) {
                currentlyPaused = false;
                sound.Play();
            }
        }
        if(!currentlyPaused)
        {
            if ((player.transform.position - transform.position).magnitude > distance)
            {
                currentlyPaused = true;
                sound.Pause();
            }
        }
    }
}
