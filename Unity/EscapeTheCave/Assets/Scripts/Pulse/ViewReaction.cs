using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reaction for blur and Vignette.
/// </summary>
public class ViewReaction
{
    private static ViewReaction instance;

    public GameObject firstPersonCamera;
    private CameraEffectsScript cameraEffects;

    public static ViewReaction GetInstance()
    {
        if (instance == null)
        {
            instance = new ViewReaction();
        }

        return instance;
    }

    // Use this for initialization
    private ViewReaction()
    {
        cameraEffects = GameObject.Find("FirstPersonCharacter").GetComponent<CameraEffectsScript>();
    }

    public void ReactionLowIntensity(float pulse)
    {
        cameraEffects.ChangeBlur(false);
        cameraEffects.ChangeMotionBlur(false);
        cameraEffects.ChangeVignette(false);
        cameraEffects.ChangeVignettePulsation(false);
    }

    public void ReactionMediumIntensity(float pulse)
    {
        cameraEffects.ChangeBlur(true);
        cameraEffects.ChangeMotionBlur(false);
        float blurIntensity = Utilities.Norm(pulse, 80, 120, 0, 5);
        cameraEffects.SetBlurIntensity(blurIntensity);

        cameraEffects.ChangeVignette(true);
        cameraEffects.ChangeVignettePulsation(false);
        float vignetteIntensity = Utilities.Norm(pulse, 80, 120, 0, 0.35f);
        cameraEffects.SetVignetteIntensity(vignetteIntensity);
    }

    public void ReactionHighIntensity(float pulse)
    {
        cameraEffects.ChangeBlur(true);
        cameraEffects.ChangeMotionBlur(true);
        float blurIntensity = Utilities.Norm(pulse, 120, 180, 5, 10);
        cameraEffects.SetBlurIntensity(blurIntensity);
        cameraEffects.SetMotionBlurIntensity(blurIntensity);

        if (!cameraEffects.IsVignettePulsation())
            StartVignettePulsationOnce();

        cameraEffects.ChangeVignette(true);
        float vignetteIntensity = Utilities.Norm(pulse, 120, 150, 0.35f, 0.45f);

        cameraEffects.SetVignettePulsationIntensityAndFrequency(vignetteIntensity, 0.5f);
    }

    public void StartVignettePulsationOnce()
    {
        cameraEffects.ChangeVignettePulsation(true);
        cameraEffects.DoVignetteIntensitySmoothPulsation();
    }
}
