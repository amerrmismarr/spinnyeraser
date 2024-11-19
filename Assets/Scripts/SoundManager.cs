using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    private AudioSource audioSource;

    public void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

        }

        audioSource = GetComponent<AudioSource>();
    }


    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("No Clip");
        }
        else
        {
            audioSource.PlayOneShot(clip);

        }

    }

}