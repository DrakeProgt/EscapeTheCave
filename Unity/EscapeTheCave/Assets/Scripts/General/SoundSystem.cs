using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundSystem
{
    static System.Random rnd = new System.Random();
    static GameObject soundFX = GameObject.Find("SoundSystem");

    public static void PlaySound(string file, float delay = 0, float volume = 1, float destroyDelay = 10, float stereoPan = 0, GameObject child = null)
    {
        if(child == null)
        {
            child = soundFX;
        }
        AudioSource a = (AudioSource)child.AddComponent<AudioSource>();
        a.clip = (AudioClip)Resources.Load(file);
        a.PlayDelayed(delay);
        a.spatialBlend = 1;
        a.volume = volume;
        a.panStereo = stereoPan;
        DestroyComponent(a, destroyDelay);
    }

    public static void PlayGearSound(GameObject child, float delay = 0, float volume = 1)
    {
        int r = rnd.Next(1, 4);
        PlaySound("Audio/Cave/FX/gear/Gear (" + r + ")", delay, volume, 10, 0, child);
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

    public static void PlayRandomMonsterSound(float delay = 0)
    {
        string fileName = "";
        int r = rnd.Next(1, 8);
        if (rnd.Next(5) == 0)
        {
            fileName = "Audio/Cave/Monster/Monster-Scream (" + r + ")";
        }
        else
        {
            fileName = "Audio/Cave/Monster/Monster-Growl (" + r + ")";
        }

        PlaySound(fileName, 0, .05f, 10, r);


        AudioReverbFilter b = (AudioReverbFilter)soundFX.AddComponent<AudioReverbFilter>();
        if(b != null)
        {
            b.room = -700;
            DestroyComponent(b, 9);
        }
    }

    static void DestroyComponent(Component component, float delay = 10)
    {
        UnityEngine.Object.Destroy(component, delay);
    }
}
