using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundEndText : MonoBehaviour
{
    public int countdownTimer;
    public Text countdown;

    string text;
    bool coroutineStarted = false;

    private void Start()
    {
        text = this.GetComponent<Text>().text;
        this.GetComponent<Text>().text = "";
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
        int temp = countdownTimer;

        while (countdownTimer > 0)
        {
            countdown.text = text + countdownTimer.ToString();

            yield return new WaitForSeconds(1f);

            countdownTimer--;
        }

        coroutineStarted = false;
        countdownTimer = temp;
    }
}
