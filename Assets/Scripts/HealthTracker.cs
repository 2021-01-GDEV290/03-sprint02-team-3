using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int health;

    public void Damage(int damage)
    {
        health = health - damage;
        if(health <= 0) this.gameObject.SetActive(false);
    }
}
