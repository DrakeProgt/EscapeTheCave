using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SoundReaction : Reaction
{
    private static SoundReaction instance;
    private AudioSource audioSource;
    private Timer t;
    private int playEffectDelay;
    System.Random random;
    private bool startEffect;
    AudioClip[] audioClips;

    public static SoundReaction GetInstance()
    {
        if (instance == null)
        {
            instance = new SoundReaction();
        }

        return instance;
    }

    private SoundReaction()
    {
        audioSource = GameObject.Find("MonsterSound").GetComponent<AudioSource>();
        startEffect = false;
        random = new System.Random();
        playEffectDelay = GetRandomInt(20000, 60000);
        t = new Timer(playEffectDelay);
        t.Elapsed += TimerElapsed;
        t.Enabled = true;

        audioClips = new AudioClip[3]
        {
            (AudioClip)Resources.Load(@"Effects\Monster_01"),
            (AudioClip)Resources.Load(@"Effects\Monster_02"),
            (AudioClip)Resources.Load(@"Effects\Monster_03"),
        };
    }

    private void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        startEffect = true;
    }

    private int GetRandomInt(int min, int max)
    {
        return random.Next(min, max);
    }

    public override void ReactionLowIntensity(float currentPulse)
    {
        if (startEffect)
        {
            audioSource.clip = audioClips[GetRandomInt(0, audioClips.Length)];
            audioSource.Play();
            startEffect = false;
            t.Interval = GetRandomInt(20000, 60000);
            t.Enabled = true;
        }
    }

    public override void ReactionMediumIntensity(float currentPulse)
    {
        if (startEffect)
        {
            audioSource.clip = audioClips[GetRandomInt(0, audioClips.Length)];
            audioSource.Play();
            startEffect = false;
            t.Interval = GetRandomInt(60000, 180000);
            t.Enabled = true;
        }
    }

    public override void ReactionHighIntensity(float currentPulse)
    {

    }
}
