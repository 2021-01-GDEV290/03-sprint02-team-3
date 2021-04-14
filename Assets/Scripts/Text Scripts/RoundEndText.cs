using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndText : MonoBehaviour
{
    ZombieSpawning zombieScript;
    float countdownTimer;
    public Text countdown;

    string text;
    bool coroutineStarted = false;

    private void Start()
    {
        zombieScript = GameObject.Find("Main Camera").GetComponent<ZombieSpawning>();
        countdownTimer = zombieScript.nextRoundDelay;
        text = GetComponent<Text>().text;
        GetComponent<Text>().text = "";
    }

    void Update()
    {
        if(ZombieSpawning.zombiesKilledThisRound == ZombieSpawning.maxZombieSpawnsThisRound && !coroutineStarted)
        {
            StartCoroutine(CountdownToNextRound());
            coroutineStarted = true;
        } else
        {
            if(!coroutineStarted)
            {
                this.GetComponent<Text>().text = "";
            }
        }
    }

    IEnumerator CountdownToNextRound()
    {
        float temp = countdownTimer;

        while (countdownTimer > 0)
        {
            countdown.text = text + countdownTimer.ToString("#.0");

            yield return new WaitForSeconds(.1f);

            countdownTimer -= .1f;
        }

        coroutineStarted = false;
        countdownTimer = temp;
    }
}
