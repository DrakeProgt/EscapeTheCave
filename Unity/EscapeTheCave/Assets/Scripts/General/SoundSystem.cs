using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundSystem
{

    static GameObject soundFX = GameObject.Find("SoundSystem");

    public static void PlayDiaryOpenSound(float delay = 0)
    {
        AudioSource a = (AudioSource)soundFX.AddComponent<AudioSource>();
        a.clip = (AudioClip)Resources.Load("Audio/Diary/Tagebuch1-Öffnen-Schließen");
        a.PlayDelayed(delay);
        DestroyComponent(a);
    }

    public static void PlayDiaryTurnSound(float delay = 0)
    {
        AudioSource a = (AudioSource)soundFX.AddComponent<AudioSource>();
        a.clip = (AudioClip) Resources.Load("Audio/Diary/Tagebuch2-Blättern");
        a.PlayDelayed(delay);
        DestroyComponent(a);
    }

    public static void PlayGateSound(float delay = 0)
    {
        AudioSource a = (AudioSource)soundFX.AddComponent<AudioSource>();
        a.clip = (AudioClip)Resources.Load("Audio/Cave/FX/Plateau-Szene");
        a.volume = .2f;
        a.PlayDelayed(delay);
        DestroyComponent(a, 10);
    }

    public static void PlayPedestalSound(float delay = 0)
    {
        AudioSource a = (AudioSource)soundFX.AddComponent<AudioSource>();
        a.clip = (AudioClip)Resources.Load("Audio/Cave/FX/Stone3-Slide");
        a.volume = .3f;
        a.PlayDelayed(delay);
        AudioReverbFilter b = (AudioReverbFilter)soundFX.AddComponent<AudioReverbFilter>();
        b.room = -700;

        DestroyComponent(a, 10);
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

    static void DestroyComponent(Component component, float delay = 5)
    {
        UnityEngine.Object.Destroy(component, delay);
    }
}
