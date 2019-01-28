using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class SoundReaction : Reaction
{
    static SoundReaction instance;
    ParticleEffectScript particleEffect;
    Timer t;
    int playEffectDelay;
    System.Random rnd = new System.Random();
    bool startEffect;

    public static SoundReaction GetInstance()
    {
        if (instance == null)
        {
            instance = new SoundReaction();
        }

        return instance;
    }

    SoundReaction()
    {
        particleEffect = GameObject.Find("FirstPersonCharacter").GetComponent<ParticleEffectScript>();
        startEffect = false;
        playEffectDelay = GetRandomInt(2000, 6000);
        t = new Timer(playEffectDelay);
        t.Elapsed += TimerElapsed;
        t.Enabled = true;
    }

    void TimerElapsed(object sender, ElapsedEventArgs e)
    {
        startEffect = true;
    }

    int GetRandomInt(int min, int max)
    {
        return rnd.Next(min, max);
    }

    void PlayMonsterSound()
    {
        int fileSelection = 0;
        if (GameManager.secondCaveReached)
        {
            fileSelection = rnd.Next(5, 12);
        }
        else
        {
            fileSelection = rnd.Next(1, 5);
        }
        SoundSystem.PlayRandomMonsterSound(GameObject.Find("MonsterAudioPoint (" + fileSelection + ")"));
    }

    public override void ReactionLowIntensity(float currentPulse)
    {
        SoundSystem.PlayBreathingSound(false);
        SoundSystem.PlayHeartBeat(false);
        if (startEffect) 
        {
            PlayMonsterSound();
            startEffect = false;
            t.Interval = GetRandomInt(180000, 240000);
//            t.Interval = GetRandomInt(3000, 6000);
            t.Enabled = true;
        }
    }

    public override void ReactionMediumIntensity(float currentPulse)
    {
        SoundSystem.PlayHeartBeat(false);
        SoundSystem.PlayBreathingSound(false);
        if (startEffect)
        {
            PlayMonsterSound();
            startEffect = false;
            t.Interval = GetRandomInt(240000, 300000);
            t.Enabled = true;
        }
    }

    public override void ReactionHighIntensity(float currentPulse)
    {
        SoundSystem.PlayHeartBeat(true);
        SoundSystem.PlayBreathingSound(true);
        if (startEffect)
        {
            PlayMonsterSound();
            startEffect = false;
            t.Interval = GetRandomInt(300000, 420000);
            t.Enabled = true;
        }
    }
}
