using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundSystem
{

    static GameObject soundFX = GameObject.Find("SoundSystem");

    public static void PlaySound(string file, float delay = 0, float volume = 1, float destroyDelay = 10, float stereoPan = 0)
    {
        AudioSource a = (AudioSource)soundFX.AddComponent<AudioSource>();
        a.clip = (AudioClip)Resources.Load(file);
        a.PlayDelayed(delay);
        a.volume = volume;
        a.panStereo = stereoPan;
        DestroyComponent(a, destroyDelay);
    }

    public static void PlayPedestalSound(float delay = 0)
    {
        PlaySound("Audio/Cave/FX/Stone3-Slide", 0, .3f, 10);
        AudioReverbFilter b = (AudioReverbFilter)soundFX.AddComponent<AudioReverbFilter>();
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
        System.Random rnd = new System.Random();
        int r = rnd.Next(4);
        PlaySound("Audio/Cave/FX/dust/Stone_Dust_" + r, 0, .7f);
    }

    public static void PlayRandomMonsterSound(float delay = 0)
    {
        string fileName = "";
        System.Random rnd = new System.Random();
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
