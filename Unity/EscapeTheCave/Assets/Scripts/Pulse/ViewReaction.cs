using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reaction for blur and Vignette.
/// </summary>
public class ViewReaction : Reaction
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

    public override void ReactionLowIntensity(float pulse)
    {        
        cameraEffects.ChangeBlur(false);
        cameraEffects.ChangeMotionBlur(false);
        cameraEffects.ChangeVignettePulsation(false);
    }

    public override void ReactionMediumIntensity(float pulse)
    {
        cameraEffects.ChangeBlur(false);
        cameraEffects.ChangeMotionBlur(false);

        if (!cameraEffects.IsVignettePulsation())
        {
            StartVignettePulsationOnce(.0007f);
        }
        cameraEffects.SetVignettePulsationIntensityAndFrequency(0.25f, 0.15f, 0.5f);
    }

    public override void ReactionHighIntensity(float pulse)
    {
        cameraEffects.ChangeBlur(true);
        cameraEffects.ChangeMotionBlur(true);
        float blurIntensity = Utilities.Norm(pulse, 120, 180, 5, 10);
        cameraEffects.SetBlurIntensity(blurIntensity);
        cameraEffects.SetMotionBlurIntensity(blurIntensity);
        
        if (!cameraEffects.IsVignettePulsation())
        {
            StartVignettePulsationOnce(.002f);
        }
        cameraEffects.SetVignettePulsationIntensityAndFrequency(0.416f, 0.25f, 0.5f);
    }

    public void StartVignettePulsationOnce(float speed)
    {
        cameraEffects.ChangeVignettePulsation(true);
        cameraEffects.DoVignetteIntensitySmoothPulsation(speed);
    }
}
