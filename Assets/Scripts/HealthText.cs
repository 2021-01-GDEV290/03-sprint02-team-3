using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text txt;
    public GameObject player;

    int currentHealth;

    private void Start()
    {
        currentHealth = player.GetComponent<HealthTracker>().health;
    }

    private void Update()
    {
        if(currentHealth != player.GetComponent<HealthTracker>().health)
        {
            currentHealth = player.GetComponent<HealthTracker>().health;
            UpdateHealth();
        }
    }

    void UpdateHealth()
    {
        txt.GetComponent<Text>();
        txt.text = "Health: " + currentHealth;
    }
}
