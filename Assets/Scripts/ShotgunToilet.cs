using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunToilet : MonoBehaviour
{
    bool alreadyUsed;

    public float distance;
    GameObject playerGO;
    Player player;

    private void Start()
    {
        playerGO = GameObject.Find("Player");
        player = playerGO.GetComponent<Player>();
    }

    private void Update()
    {
        if ((player.transform.position - transform.position).magnitude < distance && !alreadyUsed)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.state = PlayerState.shotgun;
                player.animator.SetInteger("Current State", 2);
                player.currentAmmo = player.maxAmmo[2];
                player.remainingAmmo = player.maxAmmo[2] * 3;
                alreadyUsed = true;
            }
        }
    }
}
