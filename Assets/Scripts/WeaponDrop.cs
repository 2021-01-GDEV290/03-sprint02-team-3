using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public string weaponType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            switch(weaponType)
            {
                case "Assault Rifle":
                    player.state = PlayerState.assaultRifle;
                    player.animator.SetInteger("Current State", 1);
                    break;
                case "Shotgun":
                    player.state = PlayerState.shotgun;
                    player.animator.SetInteger("Current State", 2);
                    break;
                case "Minigun":
                    player.state = PlayerState.minigun;
                    player.animator.SetInteger("Current State", 3);
                    break;
            }
            Destroy(gameObject);
        }
    }
}
