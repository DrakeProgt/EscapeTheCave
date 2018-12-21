using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class StoneDustReaction : Reaction
{
    private static StoneDustReaction instance;
    private ParticleEffectScript particleEffect;
    private Timer t;
    private int playEffectDelay;
    System.Random random;
    private bool startEffect;

    public static StoneDustReaction GetInstance()
    {
        if (instance == null)
        {
            instance = new StoneDustReaction();
        }

        return instance;
    }

    private StoneDustReaction()
    {
        particleEffect = GameObject.Find("FirstPersonCharacter").GetComponent<ParticleEffectScript>();
        startEffect = false;
        random = new System.Random();
        playEffectDelay = GetRandomInt(20000, 60000);
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
            particleEffect.PlayStoneDustOnceRandom();
            startEffect = false;
            t.Interval = GetRandomInt(20000, 60000);
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
