using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiePoints : MonoBehaviour
{
    public int points;

    public void AddPoints()
    {
        PointsTracker.points += points;
    }
}
