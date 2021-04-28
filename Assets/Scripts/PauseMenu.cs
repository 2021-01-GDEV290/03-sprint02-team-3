using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Animator trasition;
    public float trasitionTime;

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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitToMenu()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    public void CloseApplication()
    {
        Application.Quit();
    }
    IEnumerator QuitGameCoroutine()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        trasition.SetTrigger("Start");
        GameObject.Find("Player").GetComponent<Player>().enabled = false;
        yield return new WaitForSeconds(trasitionTime);
        GameIsPaused = false;
        ClearGameProgress();
        SceneManager.LoadScene("TitleScreen");
    }

    void ClearGameProgress()
    {
        ZombieSpawning.ResetGame(); // Resets the game
        Player.points = 0; // Set points to 0
    }
}
