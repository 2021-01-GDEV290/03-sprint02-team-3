using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public AudioMixer[] audioMixers; // 0 is music, 1 is sound
    public Slider[] sliders;

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public Animator trasition;
    public float trasitionTime;

    private void Start()
    {
        sliders[0].value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sliders[1].value = PlayerPrefs.GetFloat("SoundVolume", 0.75f);
    }

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

    public void SetMusicVolume()
    {
        float volume = sliders[0].value;
        audioMixers[0].SetFloat("volume", (Mathf.Log10(volume) * 20) - 10);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSoundVolume()
    {
        float volume = sliders[1].value;
        audioMixers[1].SetFloat("volume", (Mathf.Log10(volume) * 20) - 10);
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }

    void ClearGameProgress()
    {
        ZombieSpawning.ResetGame(); // Resets the game
        Player.points = 0; // Set points to 0
    }
}
