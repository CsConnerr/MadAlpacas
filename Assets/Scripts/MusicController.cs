using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;

    static MusicController instance = null;

    // Start is called before the first frame update
    void Start()
    {

        AudioClip musicClip = musicClips[Random.Range(0, musicClips.Length)];
        audioSource.clip = musicClip;
        audioSource.Play();

    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

}
