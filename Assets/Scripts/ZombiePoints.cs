using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePoints : MonoBehaviour
{
    void Update()
    {
        if(this.gameObject.GetComponent<HealthTracker>().health <= 0)
        {
            PointsTracker.points++;
        }
    }
}
