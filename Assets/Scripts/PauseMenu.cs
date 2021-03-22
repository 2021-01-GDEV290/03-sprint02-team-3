using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Animator trasition;
    public float trasitionTime = 2f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadOptionsMenu()
    {
        SceneManager.LoadScene("Options");
    }

    public void QuitGame()
    {
        StartCoroutine(QuitGameCoroutine());
    }
    IEnumerator QuitGameCoroutine()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        trasition.SetTrigger("Start");
        GameObject.Find("Player").GetComponent<PlayerController>().enabled = false;
        yield return new WaitForSeconds(trasitionTime);
        GameIsPaused = false;
        ClearGameProgress();
        SceneManager.LoadScene("TitleScreen");
    }

    void ClearGameProgress()
    {
        ZombieSpawning.ResetGame(); //Reset round
        PointsTracker.points = 0;
    }
}
