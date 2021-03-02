﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    public int maxHealth;

    public int health;

    private void Start()
    {
        health = maxHealth;
    }

    public void Damage(int damage)
    {
        health = health - damage;
        if(health <= 0)
        {
            if (this.gameObject.tag == "Zombie") PointsTracker.points += 500;
            Destroy(this.gameObject);
        }
    }
}
