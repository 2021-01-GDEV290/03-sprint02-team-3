using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverText : MonoBehaviour
{
    public Player player;
    string text;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        text = GetComponent<Text>().text;
        GetComponent<Text>().text = "";
    }

    private void Update()
    {
        if (player.health <= 0)
        {
            GetComponent<Text>().text = text;
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("Scoreboard");
            }
        }
        else
        {
            GetComponent<Text>().text = "";
        }
    }
}
