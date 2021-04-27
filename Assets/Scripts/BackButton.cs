using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public Animator transition;
    public float transitionTime;

    public void ReturnToTitleScreen(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        ScoreboardTracker.score = 0;
        ScoreboardTracker.kills = 0;
        ScoreboardTracker.damageTaken = 0;
        ScoreboardTracker.damageDealt = 0;
        SceneManager.LoadScene(sceneName);
    }
}
