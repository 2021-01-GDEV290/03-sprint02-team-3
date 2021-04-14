using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardCopy : MonoBehaviour
{
    public string type; // What stat is this?
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        switch (type)
        {
            case "Score":
                text.text = "" + ScoreboardTracker.score;
                break;
            case "Kills":
                text.text = "" + ScoreboardTracker.kills;
                break;
            case "Damage Taken":
                text.text = "" + ScoreboardTracker.damageTaken;
                break;
            case "Damage Dealt":
                text.text = "" + ScoreboardTracker.damageDealt;
                break;
        }
    }
}
