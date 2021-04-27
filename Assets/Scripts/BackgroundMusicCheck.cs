using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicCheck : MonoBehaviour
{
    GameObject[] musicGOs;

    void Start()
    {
        musicGOs = GameObject.FindGameObjectsWithTag("MusicPlayer");
        for(int i = musicGOs.Length - 1; i > 0; i--)
        {
            Destroy(musicGOs[i]);
        }
        
        
    }
}
