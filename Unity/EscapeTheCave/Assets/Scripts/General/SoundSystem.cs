using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundSystem
{
    static System.Random rnd = new System.Random();
    static GameObject soundFX = GameObject.Find("SoundSystem");

    public static void PlaySound(string file, float delay = 0, float volume = 1, float destroyDelay = 10, float stereoPan = 0, GameObject child = null, float spatialBlend = 1)
    {
        if (child == null)
        {
            child = soundFX;
        }
        AudioSource a = (AudioSource)child.AddComponent<AudioSource>();
        a.clip = (AudioClip)Resources.Load(file);
        a.PlayDelayed(delay);
        a.spatialBlend = spatialBlend;
        a.volume = volume;
        a.panStereo = stereoPan;
        DestroyComponent(a, destroyDelay);
    }

    public static void PlayScream(int number, float volume = 1, float delay = 0, GameObject child = null)
    {
        if (child == null) child = GameManager.Player;
        PlaySound("Audio/Player/Scream/Scream_0" + number, delay, volume, 10, 0, child);
    }

    public static void PlayGearSound(GameObject child, float delay = 0, float volume = 1)
    {
        int r = rnd.Next(1, 4);
        PlaySound("Audio/Cave/FX/gear/Gear (" + r + ")", delay, volume, 10, 0, child);
        if (child.GetComponent<AudioReverbFilter>() != null)
        {
            AudioReverbFilter b = (AudioReverbFilter)child.AddComponent<AudioReverbFilter>();
            b.room = -700;
            DestroyComponent(b, 9);
        }
    }

    public static void PlayPedestalSound(GameObject child, float delay = 0, float volume = 1)
    {
        PlaySound("Audio/Cave/FX/Stone3-Slide", delay, volume, 10, 0, child);
        AudioReverbFilter b = (AudioReverbFilter)child.AddComponent<AudioReverbFilter>();
        b.room = -700;
        DestroyComponent(b, 9);
    }

    public static void PlayStonePress(float delay = 0)
    {
        AudioSource a = (AudioSource)soundFX.AddComponent<AudioSource>();
        a.clip = (AudioClip)Resources.Load("Audio/Cave/FX/Stone1-Crack");
        a.volume = .2f;
        a.PlayDelayed(delay);

        DestroyComponent(a, 10);
    }

    public static void PlayDustSound(float delay = 0)
    {
        int r = rnd.Next(4);
        PlaySound("Audio/Cave/FX/dust/Stone_Dust_" + r, delay, .7f);
    }

    public static void PlayRandomMonsterSound(GameObject child, float delay = 0)
    {
        string fileName = "";
        int r = rnd.Next(100);
        if (r < 20)
        {
            fileName = "Audio/Cave/Monster/Monster-Scream (" + rnd.Next(1, 8) + ")";
        }
        else if (r > 10 && r < 30)
        {
            fileName = "Audio/Cave/Monster/MonsterSchritt (" + rnd.Next(1, 4) + ")";
        }
        else
        {
            fileName = "Audio/Cave/Monster/Monster-Growl (" + rnd.Next(1, 8) + ")";
        }

        PlaySound(fileName, 0, 1, 10, (float)rnd.NextDouble() * -2 + 1, child);


        if (soundFX.GetComponent<AudioReverbFilter>() != null)
        {
            AudioReverbFilter b = (AudioReverbFilter)soundFX.AddComponent<AudioReverbFilter>();
            b.room = -1200;
            DestroyComponent(b, 9);
        }
    }

    public static void PlayHeartBeat(bool isPlaying)
    {
        GameObject o = GameObject.Find("HeartbeatSound");
        AudioSource a = o.GetComponent<AudioSource>();

        if (isPlaying)
        {
            if (a == null)
            {
                a = o.AddComponent<AudioSource>();
            }
        }

        if(a == null || a.isPlaying)
        {
            return;
        }

        a.clip = (AudioClip)Resources.Load("Audio/Player/Heartbeats/Heartbeat_" + rnd.Next(1, 7));

        if(isPlaying)
        {
            a.volume = .8f;

        }
        else
        {
            a.volume -= .15f;
        }
        a.Play();

        if (a.volume <= .1f)
        {
            DestroyComponent(a, 0);
        }
    }

    public static void PlayBreathingSound(bool isPlaying)
    {
        GameObject o = GameObject.Find("BreathingSound");
        if (isPlaying)
        {
            if (o.GetComponent<AudioSource>() != null)
            {
                return;
            }
            AudioSource a = o.AddComponent<AudioSource>();
            a.clip = (AudioClip)Resources.Load("Audio/Player/Slow_Breath");
            a.loop = true;
            a.volume = .8f;
            a.Play();
        }
        else
        {
            if (o.GetComponent<AudioSource>() == null)
            {
                return;
            }

            DestroyComponent(o.GetComponent<AudioSource>(), 0);
        }
    }

    static void DestroyComponent(Component component, float delay = 10)
    {
        UnityEngine.Object.Destroy(component, delay);
    }
}
