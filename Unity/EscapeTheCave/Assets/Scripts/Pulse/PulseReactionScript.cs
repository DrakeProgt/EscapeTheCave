using System;
using System.Collections;
using System.Collections.Generic;
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

    private ViewReaction viewReaction;

    //Use this for initialization
    void Start()
    {
        currentPulse = 60;
        pulseLevel = PulseLevel.low;
        fakePulse = FakePulse.GetInstance();
        viewReaction = ViewReaction.GetInstance();
        fakePulse.Init();
        StartCoroutine(fakePulse.PulseLoop());
    }
	
	//Update is called once per frame
	void FixedUpdate()
    {
        currentPulse = GetLivePulseData();
        SetPulseLevel(currentPulse);

        ReactView();
        ReactStonesAndDust();

        //GameObject.Find("Puls").GetComponent<Text>().text = currentPulse.ToString();
    }

    private void ReactStonesAndDust()
    {

    }

    private void ReactView()
    {
        switch (pulseLevel)
        {
            case PulseLevel.low:
                viewReaction.ReactionLowIntensity(currentPulse);
                break;
            case PulseLevel.medium:
                viewReaction.ReactionMediumIntensity(currentPulse);
                break;
            case PulseLevel.high:
                viewReaction.ReactionHighIntensity(currentPulse);
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
