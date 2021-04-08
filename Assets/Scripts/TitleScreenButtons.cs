using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    public Animator trasition;
    public float trasitionTime = 2f;
    public void StartButton(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        trasition.SetTrigger("Start");
        yield return new WaitForSeconds(trasitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("SettingsMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
