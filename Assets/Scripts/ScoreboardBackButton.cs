using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreboardBackButton : MonoBehaviour
{
    public AudioSource sound;
    public Animator transition;
    public float transitionTime;

    public void ReturnToTitleScreen(string sceneName)
    {
        sound.Play();
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}
