using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroSequence : MonoBehaviour
{

    public bool LoadGame = false;
    VideoPlayer v1;

    // Use this for initialization
    void Start()
    {
        v1 = gameObject.GetComponent<VideoPlayer>();
        v1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!v1.isPlaying)
        {
            if (LoadGame)
            {
                SceneManager.LoadScene("Scenes/Main");
            }
            else
            {
                SceneManager.LoadScene("Scenes/MainMenu"); 
            }
        }
    }
}
