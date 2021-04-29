using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioSource sound;
    public AudioMixer[] audioMixers; // 0 is music, 1 is sound
    public Dropdown resolutionDropdown;

    public Slider[] sliders;

    public Animator transition;
    public float transitionTime = 2f;

    Resolution[] resolutions;

    private void Start()
    {
        sliders[0].value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        sliders[1].value = PlayerPrefs.GetFloat("SoundVolume", 0.75f);

        resolutions = resolutions = Screen.resolutions.Select(resolution => new Resolution 
        { 
            width = resolution.width, height = resolution.height 
        }
        ).Distinct().ToArray();
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if(resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
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

    public void SetFullscreen(bool isFullscreen)
    {
        sound.Play();
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

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
