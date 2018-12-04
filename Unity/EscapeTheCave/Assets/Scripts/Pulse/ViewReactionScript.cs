using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PulseLevel
{
    low = 0,
    medium = 1,
    high = 2
}

/// <summary>
/// Reaction for blur and Vignette.
/// </summary>
public class ViewReactionScript : MonoBehaviour
{
    private float currentPulse;
    private PulseLevel pulseLevel;
    private FakePulseScript fakePulse;

    public GameObject firstPersonCamera;
    private CameraEffectsScript cameraEffects;

    // Use this for initialization
    void Start()
    {
        cameraEffects = firstPersonCamera.GetComponent<CameraEffectsScript>();
        currentPulse = 60;
        pulseLevel = PulseLevel.low;
        fakePulse = FakePulseScript.GetInstance();
        fakePulse.Init();
        StartCoroutine(fakePulse.PulseLoop());
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        currentPulse = GetLivePulseData();
        SetPulseLevel(currentPulse);
        React();
    }

    private void React()
    {
        switch (pulseLevel)
        {
            case PulseLevel.low:
                ReactionLowIntensity();
                break;
            case PulseLevel.medium:
                ReactionMediumIntensity();
                break;
            case PulseLevel.high:
                ReactionHighIntensity();
                break;
            default:
                break;
        }
    }

    private void ReactionLowIntensity()
    {
        cameraEffects.ChangeBlur(false);
        cameraEffects.ChangeMotionBlur(false);
        cameraEffects.ChangeVignette(false);
        cameraEffects.ChangeVignettePulsation(false);
    }

    private void ReactionMediumIntensity()
    {
        cameraEffects.ChangeBlur(true);
        cameraEffects.ChangeMotionBlur(false);
        float blurIntensity = Utilities.Norm(currentPulse, 80, 120, 0, 5);
        cameraEffects.SetBlurIntensity(blurIntensity);

        cameraEffects.ChangeVignette(true);
        cameraEffects.ChangeVignettePulsation(false);
        float vignetteIntensity = Utilities.Norm(currentPulse, 80, 120, 0, 0.35f);
        cameraEffects.SetVignetteIntensity(vignetteIntensity);
    }

    private void ReactionHighIntensity()
    {
        cameraEffects.ChangeBlur(true);
        cameraEffects.ChangeMotionBlur(true);
        float blurIntensity = Utilities.Norm(currentPulse, 120, 180, 5, 10);
        cameraEffects.SetBlurIntensity(blurIntensity);
        cameraEffects.SetMotionBlurIntensity(blurIntensity);

        if (!cameraEffects.IsVignettePulsation())
            StartVignettePulsationOnce();

        cameraEffects.ChangeVignette(true);
        float vignetteIntensity = Utilities.Norm(currentPulse, 120, 150, 0.35f, 0.45f);

        cameraEffects.SetVignettePulsationIntensityAndFrequency(vignetteIntensity, 0.5f);
    }

    private void StartVignettePulsationOnce()
    {
        cameraEffects.ChangeVignettePulsation(true);
        StartCoroutine(cameraEffects.SetVignetteIntensitySmoothPulsation());
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
