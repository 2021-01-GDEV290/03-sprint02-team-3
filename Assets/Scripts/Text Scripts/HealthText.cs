using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text txt;
    public Player player;

    int currentHealth;

    private void Update()
    {
        txt.GetComponent<Text>();
        txt.text = "Health: " + player.health;
    }
}
