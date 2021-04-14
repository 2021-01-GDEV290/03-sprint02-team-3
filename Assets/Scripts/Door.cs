using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D collider;
    private GameObject player;
    public float distance;
    public int pointsNeeded;

    public bool canBeOpened;

    private void Start()
    {
        player = GameObject.Find("Player");
        canBeOpened = false;
        collider = GetComponent<Collider2D>();
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
                Player.points -= pointsNeeded;
                Debug.Log("Door has been bought for " + pointsNeeded + " points.");
                gameObject.SetActive(false);
                AstarPath.active.UpdateGraphs(collider.bounds);
            }
        } else
        {
            canBeOpened = false;
        }
    }
}
