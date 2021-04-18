using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public int pointsNeeded;
    public float distance;
    public float ARChance;
    float tempARChance;
    public float shotgunChance;
    float tempShotgunChance;
    public float minigunChance;
    public bool canBeBought;

    GameObject player;
    Player playerScript;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<Player>();
        canBeBought = false;
        tempARChance = ARChance;
        tempShotgunChance = tempARChance + shotgunChance;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }
        if ((player.transform.position - transform.position).magnitude < distance)
        {
            canBeBought = true;
            if (Input.GetKeyDown(KeyCode.E) && Player.points >= pointsNeeded)
            {
                float thisRandom = Random.Range(0, ARChance + shotgunChance + minigunChance);
                if (thisRandom <= tempARChance)
                {
                    playerScript.state = PlayerState.assaultRifle;
                    playerScript.animator.SetInteger("Current State", 1);
                    playerScript.currentAmmo = playerScript.maxAmmo[1];
                    playerScript.remainingAmmo = playerScript.maxAmmo[1] * 2;
                }
                else if (thisRandom <= tempShotgunChance)
                {
                    playerScript.state = PlayerState.shotgun;
                    playerScript.animator.SetInteger("Current State", 2);
                    playerScript.currentAmmo = playerScript.maxAmmo[2];
                    playerScript.remainingAmmo = playerScript.maxAmmo[2] * 3;
                }
                else
                {
                    playerScript.state = PlayerState.minigun;
                    playerScript.animator.SetInteger("Current State", 3);
                    playerScript.currentAmmo = playerScript.maxAmmo[3];
                }
            }
        }
        else
        {
            canBeBought = false;
        }
    }
}