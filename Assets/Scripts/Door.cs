using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource sound;

    private GameObject player;
    public float distance;
    public int pointsNeeded;

    public bool canBeOpened;

    private void Start()
    {
        player = GameObject.Find("Player");
        canBeOpened = false;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        if ((player.transform.position - transform.position).magnitude < distance)
        {
            canBeOpened = true;
            if (Input.GetKeyDown(KeyCode.E) && Player.points >= pointsNeeded)
            {
                sound.Play();
                Player.points -= pointsNeeded;
                Debug.Log("Door has been bought for " + pointsNeeded + " points.");
                gameObject.SetActive(false);
                AstarPath.active.UpdateGraphs(GetComponent<BoxCollider2D>().bounds);
            }
        } else
        {
            canBeOpened = false;
        }
    }
}
