using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;

    static MusicController instance = null;
    private int index; // index of current song


    // Start is called before the first frame update
    void Start()
    {

        index = Random.Range(0, musicClips.Length);
        AudioClip musicClip = musicClips[index];
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

    void Update()
    {
        if (!audioSource.isPlaying)
        {           
            index = (index + 1) % musicClips.Length;
            Debug.Log("Next song " + index + 1);
            audioSource.clip = musicClips[index];
            audioSource.Play();
        }
    }

}
