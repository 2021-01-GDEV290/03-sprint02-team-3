using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject player;
    public int distance;

    public bool canBeOpened;

    private void Start()
    {
        player = GameObject.Find("Player");
        canBeOpened = false;
    }

    private void Update()
    {
        if((player.transform.position - this.transform.position).magnitude < distance)
        {
            canBeOpened = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(this.gameObject);
            }
        } else
        {
            canBeOpened = false;
        }
    }
}
