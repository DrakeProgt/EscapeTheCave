using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectScript : MonoBehaviour
{
    Vector3[] possiblePositions;
    GameObject stoneDust;

    // Use this for initialization
    private void Start()
    {
        stoneDust = GameObject.Find("DustStorm");
        possiblePositions = new Vector3[3] 
        {
            new Vector3(4.156473f, 3.93f, 2.12f),
            new Vector3(4.16f, 3.93f, -3.91f),
            new Vector3(4.16f, 3.93f, -9.02f)
        };
	}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            PlayStoneDustOnceRandom();
	}

    private void PlayStoneDustOnceRandom()
    {
        System.Random random = new System.Random();
        int index = random.Next(0, 3);
        stoneDust.GetComponent<Transform>().position = new Vector3(possiblePositions[index].x, possiblePositions[index].y, possiblePositions[index].z);
        stoneDust.GetComponent<ParticleSystem>().Play(true);
        stoneDust.GetComponentInChildren<AudioSource>().Play();
    }
}
