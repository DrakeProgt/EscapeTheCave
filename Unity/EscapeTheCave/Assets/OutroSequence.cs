using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class OutroSequence : MonoBehaviour
{

    VideoPlayer v;

    // Use this for initialization
    void Start()
    {
        v = gameObject.GetComponent<VideoPlayer>();
        v.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!v.isPlaying)
        {
            SceneManager.LoadScene("Scenes/MainMenu");
        }
    }
}
