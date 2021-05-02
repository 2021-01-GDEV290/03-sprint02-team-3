using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text txt;
    public Player player;

    private void Update()
    {
        txt.GetComponent<Text>();
        switch (player.state)
        {
            case PlayerState.normal:
                txt.text = "AMMO: " + player.currentAmmo + " / --";
                break;
            case PlayerState.assaultRifle:
                txt.text = "AMMO: " + player.currentAmmo + " / " + player.remainingAmmo;
                break;
            case PlayerState.shotgun:
                txt.text = "AMMO: " + player.currentAmmo + " / " + player.remainingAmmo;
                break;
            case PlayerState.minigun:
                txt.text = "AMMO: " + player.currentAmmo + " / 0";
                break;
        }
    }
}
