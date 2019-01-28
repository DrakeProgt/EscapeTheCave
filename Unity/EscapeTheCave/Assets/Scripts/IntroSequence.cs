using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{

    VideoPlayer v1, v2;
    bool v1Played = true, v2Played;

    // Use this for initialization
    void Start()
    {
        v1 = gameObject.GetComponents<VideoPlayer>()[0];
        v2 = gameObject.GetComponents<VideoPlayer>()[1];
        v1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!v1.isPlaying && !v2Played)
        {
            v2.Play();
            v2Played = true;
        }

        if(!v1.isPlaying && !v2.isPlaying && v1Played && v2Played)
        {
            SceneManager.LoadScene("Scenes/MainMenu");

        }
    }
}
