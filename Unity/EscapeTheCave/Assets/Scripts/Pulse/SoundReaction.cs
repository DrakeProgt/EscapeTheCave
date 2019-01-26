using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SoundReaction : Reaction
{
    private static SoundReaction instance;
    private ParticleEffectScript particleEffect;
    private Timer t;
    private int playEffectDelay;
    System.Random random;
    private bool startEffect;

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
        particleEffect = GameObject.Find("FirstPersonCharacter").GetComponent<ParticleEffectScript>();
        startEffect = false;
        random = new System.Random();
        playEffectDelay = GetRandomInt(2000, 6000);
        t = new Timer(playEffectDelay);
        t.Elapsed += TimerElapsed;
        t.Enabled = true;
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
            SoundSystem.PlayRandomMonsterSound();
            startEffect = false;
            t.Interval = GetRandomInt(6000, 6000);
            t.Enabled = true;
        }
    }

    public override void ReactionMediumIntensity(float currentPulse)
    {
        if (startEffect)
        {
            particleEffect.PlayStoneDustOnceRandom();
            startEffect = false;
            t.Interval = GetRandomInt(60000, 180000);
            t.Enabled = true;
        }
    }

    public override void ReactionHighIntensity(float currentPulse)
    {

    }
}
