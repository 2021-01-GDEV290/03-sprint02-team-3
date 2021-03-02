using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{
    public int pointsNeeded;
    public int distance;
    public bool canBeBought;
    public bool bought;

    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        canBeBought = false;
        bought = false;
    }

    private void Update()
    {
        if ((player.transform.position - this.transform.position).magnitude < distance)
        {
            canBeBought = true;
            if (Input.GetKeyDown(KeyCode.E) && PointsTracker.points >= pointsNeeded && !bought)
            {
                PointsTracker.points -= pointsNeeded;
                bought = true;
                player.gameObject.GetComponent<HealthTracker>().maxHealth = 5;
                player.gameObject.GetComponent<HealthTracker>().health = 5;
            }
        }
        else
        {
            canBeBought = false;
        }
    }
}
