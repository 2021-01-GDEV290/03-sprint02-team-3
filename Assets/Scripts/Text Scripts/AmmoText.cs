using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text txt;
    public Player player;

    int currentAmmo;

    private void Update()
    {
        if (currentAmmo != player.currentAmmo)
        {
            currentAmmo = player.currentAmmo;
            UpdateAmmo();
        }
    }

    void UpdateAmmo()
    {
        txt.GetComponent<Text>();
        txt.text = "Ammo: " + currentAmmo;
    }
}
