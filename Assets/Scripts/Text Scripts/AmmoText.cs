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
                txt.text = "Ammo: " + player.currentAmmo + " / ∞";
                break;
            case PlayerState.assaultRifle:
                txt.text = "Ammo: " + player.currentAmmo + " / " + player.remainingAmmo;
                break;
            case PlayerState.shotgun:
                txt.text = "Ammo: " + player.currentAmmo + " / " + player.remainingAmmo;
                break;
            case PlayerState.minigun:
                txt.text = "Ammo: " + player.currentAmmo + " / 0";
                break;
        }
    }
}
