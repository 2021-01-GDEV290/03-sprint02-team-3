using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardTracker : MonoBehaviour
{
    public static int score;
    public int scoreTracker;
    public static int kills;
    public int killsTracker;
    public static int damageTaken;
    public int damageTakenTracker;
    public static int damageDealt;
    public int damageDealtTracker;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        scoreTracker = score;
        killsTracker = kills;
        damageTakenTracker = damageTaken;
        damageDealtTracker = damageDealt;
    }
}
