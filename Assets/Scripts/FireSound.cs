using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : MonoBehaviour
{
    public AudioSource sound;

    public float distance;

    GameObject player;
    bool currentlyPaused;

    private void Start()
    {
        player = GameObject.Find("Player");
        currentlyPaused = true;
        sound.Play();
    }

    private void Update()
    {
        if(currentlyPaused)
        {
            if((player.transform.position - transform.position).magnitude <= distance) {
                currentlyPaused = false;
                StartCoroutine(StartFadeIn(sound, 1f, 1f));
            }
        }
        if(!currentlyPaused)
        {
            if ((player.transform.position - transform.position).magnitude > distance)
            {
                currentlyPaused = true;
                sound.Pause();
                StartCoroutine(StartFadeOut(sound, 1f, 0f));
            }
        }
    }

    IEnumerator StartFadeIn(AudioSource audioSource, float duration, float targetVolume)
    {
        audioSource.Play();
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

    IEnumerator StartFadeOut(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
