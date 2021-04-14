using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpgrade : MonoBehaviour
{
    public int pointsNeeded;
    public float distance;
    public bool canBeBought;
    public bool bought;
    public float newSpeed;

    GameObject player;
    Player playerScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        canBeBought = false;
        bought = false;
    }

    private void Update()
    {
        if(player == null)
        {
            return;
        }
        if ((player.transform.position - this.transform.position).magnitude < distance)
        {
            canBeBought = true;
            if (Input.GetKeyDown(KeyCode.E) && Player.points >= pointsNeeded && !bought)
            {
                Player.points -= pointsNeeded;
                bought = true;
                playerScript.moveSpeed = newSpeed;
            }
        }
        else
        {
            canBeBought = false;
        }
    }
}
