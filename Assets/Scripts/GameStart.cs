using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameStart : MonoBehaviour
{
    public AudioMixer[] audioMixers;

    void Start()
    {
        audioMixers[0].SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("MusicVolume")) * 20 - 10);
        audioMixers[1].SetFloat("volume", Mathf.Log10(PlayerPrefs.GetFloat("SoundVolume")) * 20 - 10);
        enabled = false;
    }
}
