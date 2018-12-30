using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Self written little helper for easy scripted animations 
public class Tweeny
{
    private float startTime = -1.0f; // in ms
    private float duration; // in ms
    private float startVal;
    private float targetVal;
    private Vector3 startVec;
    private Vector3 targetVec;
    private Vector3 deltaVec; // just helpfull and speeds up a bit
    private int tweenType;
    public bool finished = false;

    // constructors
    public Tweeny(float startVal, float targetVal, float duration, string tweenType)
    {
        this.startVal = startVal;
        this.targetVal = targetVal;
        this.duration = duration;
        setTweenType(tweenType);
    }
    
    public Tweeny(Vector3 startVec, Vector3 targetVec, float duration, string tweenType)
    {
        this.startVec = startVec;
        this.targetVec = targetVec;
        this.duration = duration;
        deltaVec = targetVec - startVec;
        setTweenType(tweenType);
    }

    private void setTweenType(String tweenTypeString)
    {
        switch (tweenTypeString)
        {
            case "linear":
                tweenType = 0;
                break;
            case "easeInOutQuad":
                tweenType = 1;
                break;
            default:
                // easeInOutQuad is my favorite, so default
                tweenType = 1;
                break;
        }
    }
    
    // methods

    public void start()
    {
        if (startTime < 0)
        {
            startTime = Time.time * 1000;
        }    
    }

    public void reset()
    {
        startTime = -1.0f;
        finished = false;
    }
    
    private bool isFinished()
    {
        if (finished) return (finished);
        if (startTime >= 0 && startTime + duration <= Time.time * 1000)
        {
            finished = true;
        }
        return finished;
    }

    public float nextValue()
    {
        start();
        isFinished();
        if (finished) return targetVal;

        return calculateNextValue(startVal, targetVal);
    }

    public Vector3 nextVector()
    {
        start();
        isFinished();
        if (finished) return targetVec;
        
        float progress = calculateNextValue(0.0f, 1.0f);

        return (startVec + (progress * deltaVec));
    }

    private float calculateNextValue(float start, float end)
    {
        switch (tweenType)
        {
            case 0:
                // linear
                return Tween.linear((Time.time * 1000) - startTime, start, end, duration);
            default:
                // easeInOutQuad
                return Tween.easeInOutQuad((Time.time * 1000) - startTime, start, end, duration);
        }
    }
}

public static class Tween
{
    // http://gizma.com/easing/
    // t: current time
    // b: start value
    // c: change in value
    // d: duration
    public static float linear(float t, float b, float c, float d)
    {
        return c * t / d + b;
    }

    public static float easeInOutQuad(float t, float b, float c, float d)
    {
        t /= d / 2;
        if (t < 1) return c / 2 * t * t + b;
        t--;
        return -c / 2 * (t * (t - 2) - 1) + b;
    }
}

