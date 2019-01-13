using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ParticleEffectScript : MonoBehaviour
{
    Vector3[] possiblePositions;
    AudioClip[] audioClips;
    GameObject stoneDust;

    // Use this for initialization
    private void Start()
    {
        stoneDust = (GameObject)GameObject.Instantiate(Resources.Load(@"Effects\DustStorm"), Vector3.zero, Quaternion.identity);
        //stoneDust = GameObject.Find("DustStorm");
        possiblePositions = new Vector3[6] 
        {
            new Vector3(2.294353f, 3.6f, 5.34f),
            new Vector3(-4.31f, 3.6f, -5.11f),
            new Vector3(16.64f, 3.6f, -5.5f),
            new Vector3(16.25f, 3.6f, 3.86f),
            new Vector3(7.59f, 3.6f, -1f),
            new Vector3(1.15f, 3.6f, -4.57f)
        };
        audioClips = new AudioClip[3]
        {
            (AudioClip)Resources.Load(@"Effects\Stone_Dust_01"),
            (AudioClip)Resources.Load(@"Effects\Stone_Dust_02"),
            (AudioClip)Resources.Load(@"Effects\Stone_Dust_03"),
        };
        PlayStoneDustOnceRandom();
    }

    public void PlayStoneDustOnceRandom()
    {
        System.Random random = new System.Random();
        int index = random.Next(0, possiblePositions.Length);
        stoneDust.GetComponent<Transform>().position = new Vector3(possiblePositions[index].x, possiblePositions[index].y, possiblePositions[index].z);
        stoneDust.GetComponent<ParticleSystem>().Play(true);

        //sound
        index = random.Next(0, audioClips.Length);
        AudioSource audio = stoneDust.GetComponentInChildren<AudioSource>();
        audio.clip = audioClips[index];
        audio.Play();

        //controller vibration
        float stoneDustDir = Utilities.GetDirection(UnityEngine.Camera.main.transform, stoneDust.transform);
        float vibLeft = (stoneDustDir == -1 || stoneDustDir == 0) ? 1 : 0;
        float vibRight = (stoneDustDir == 1 || stoneDustDir == 0) ? 1 : 0;
        StartCoroutine(Utilities.ControllerVibration(vibLeft, vibRight, 2));
    }
}
