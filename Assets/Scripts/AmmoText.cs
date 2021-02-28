using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoText : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text txt;
    public GameObject player;

    int currentAmmo;

    private void Start()
    {
        currentAmmo = player.GetComponent<Shooting>().currentAmmo;
    }

    private void Update()
    {
        if (currentAmmo != player.GetComponent<Shooting>().currentAmmo)
        {
            currentAmmo = player.GetComponent<Shooting>().currentAmmo;
            UpdateAmmo();
        }
    }

    void UpdateAmmo()
    {
        txt.GetComponent<Text>();
        txt.text = "Ammo: " + currentAmmo;
    }
}
