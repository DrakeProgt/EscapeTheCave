﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public enum PulseLevel
{
    low = 0,
    medium = 1,
    high = 2
}

/// <summary>
/// Main class for getting and categorizing pulse
/// </summary>
public class PulseReactionScript : MonoBehaviour
{
    private float currentPulse;
    private PulseLevel pulseLevel;
    private FakePulse fakePulse;
    private RealPulse realPulse;

    private List<Reaction> reactions;

    //Use this for initialization
    void Start()
    {
        /*
        currentPulse = 60;
        pulseLevel = PulseLevel.low;
        reactions = new List<Reaction>();
        reactions.Add(ViewReaction.GetInstance());
        reactions.Add(StoneDustReaction.GetInstance());

        realPulse = RealPulse.GetInstance();
        realPulse.init(33333);

        Thread receiveThread = new Thread(
           new ThreadStart(realPulse.ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
        */

        
        currentPulse = 60;
        pulseLevel = PulseLevel.low;
        fakePulse = FakePulse.GetInstance();
        reactions = new List<Reaction>();
        reactions.Add(ViewReaction.GetInstance());
        reactions.Add(SoundReaction.GetInstance());
        reactions.Add(StoneDustReaction.GetInstance());
        fakePulse.Init();
//        StartCoroutine(fakePulse.PulseLoop());
        StartCoroutine(fakePulse.PulseAdvancedRandom());
        
    }
	//Update is called once per frame
	void FixedUpdate()
    {
        //currentPulse = realPulse.getHR();
        SetPulseLevel(GetLivePulseData());

        React();
        //GameObject.Find("Puls").GetComponent<Text>().text = currentPulse.ToString();
    }

    private void React()
    {
        switch (pulseLevel)
        {
            case PulseLevel.low:
                reactions.ForEach(r => r.ReactionLowIntensity(currentPulse));
                break;
            case PulseLevel.medium:
                reactions.ForEach(r => r.ReactionMediumIntensity(currentPulse));
                break;
            case PulseLevel.high:
                reactions.ForEach(r => r.ReactionHighIntensity(currentPulse));
                break;
            default:
                break;
        }
    }

    private void SetPulseLevel(double pulse)
    {
        if (pulse <= 80)
        {
            pulseLevel = PulseLevel.low;
        }
        else if (pulse <= 120)
        {
            pulseLevel = PulseLevel.medium;
        }
        else if (pulse > 120)
        {
            pulseLevel = PulseLevel.high;
        }
    }

    private float GetLivePulseData()
    {
        return fakePulse.GetLivePulse();
    }
}
