using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public Animator animator;

    public string weaponType;
    public float timeUntilDestroy;

    public float slowFlashTime; // When does the animator trigger the slow flash animation
    public float fastFlashTime; // When does the animator trigger the fast flash animation

    public static int ARStockAmmoMultiplier = 2;
    public static int shotgunStockAmmoMultiplier = 3;

    private void Awake()
    {
        InvokeRepeating("WaitUntilDestroy", 1f, 1f);
    }

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
                    player.currentAmmo = player.maxAmmo[1];
                    player.remainingAmmo = player.maxAmmo[1] * ARStockAmmoMultiplier;
                    break;
                case "Shotgun":
                    player.state = PlayerState.shotgun;
                    player.animator.SetInteger("Current State", 2);
                    player.currentAmmo = player.maxAmmo[2];
                    player.remainingAmmo = player.maxAmmo[2] * shotgunStockAmmoMultiplier;
                    break;
                case "Minigun":
                    player.state = PlayerState.minigun;
                    player.animator.SetInteger("Current State", 3);
                    player.currentAmmo = player.maxAmmo[3];
                    break;
            }
            Destroy(gameObject);
        }
    }

    void WaitUntilDestroy()
    {
        timeUntilDestroy--;
        if (timeUntilDestroy <= 0)
        {
            Destroy(gameObject);
        }
        if(timeUntilDestroy <= 2.5f)
        {
            animator.SetBool("Eight", true);
            return;
        }
        if(timeUntilDestroy <= 7.5f)
        {
            animator.SetBool("Halfway", true);
            return;
        }
    }
}
