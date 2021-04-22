using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public AudioSource[] sounds;
    public Animator transition;
    public float transitionTime = 2f;
    public void StartButton(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
        sounds[0].Play();
    }
    public void SettingsButton(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
        sounds[1].Play();
    }

    IEnumerator LoadLevel(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
