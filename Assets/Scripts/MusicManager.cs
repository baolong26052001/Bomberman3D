using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource myAudioSource;
    [SerializeField] private AudioClip[] songArray;
    private int songIndex = 0;

    private void Awake()
    {   
        // Singleton for MusicManager
        if (FindObjectsOfType<MusicManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        myAudioSource = GetComponent<AudioSource>();        
    }

    private void Update()
    {
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.clip = songArray[songIndex];
            myAudioSource.Play();
            if (songIndex < songArray.Length - 1)
            {
                songIndex++;
            }       
            else
            {
                songIndex = 0;
            }
        }        
    }
}
