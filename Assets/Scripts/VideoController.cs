using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    public string videoPlayerURL;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        GameObject camera = GameObject.Find("Main Camera"); // main camera
        videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.playOnAwake = false; // don't play at start
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane; // puts video in front of scene
        videoPlayer.url = videoPlayerURL; // path / url to video (ex: "Assets/Level Assets/Level 3/video hint.mp4")
    }

    void Update()
    {
        if (Input.anyKey && videoPlayer.isPlaying)
        { // stop video with any key press
            videoPlayer.Stop();
        }
    }

    public void playVideo()
    {
        Debug.Log("playing video");
        videoPlayer.Play();
    }

    void EndReached()
    {
        videoPlayer.Stop();
    }
}
