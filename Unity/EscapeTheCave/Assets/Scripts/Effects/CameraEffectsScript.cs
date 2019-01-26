using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using UnityEngine.PostProcessing;
using Assets.Scripts;

public class CameraEffectsScript : MonoBehaviour
{
    private BlurOptimized blur;
    private MotionBlur motionBlur;
    private VignetteModel vignette;
    private bool bPulsation, reachedHigh, reachedLow = true;
    float maxIntensity, minIntensity, vignetteIntensity;
    float frequency;

    // Use this for initialization
    private void Start()
    {
        bPulsation = false;
        //use Camera.main or gameObject
        motionBlur = gameObject.AddComponent<MotionBlur>();
        blur = gameObject.AddComponent<BlurOptimized>();

        //TODO: create in script with 'AddComponent' and set profile in script?
        vignette = gameObject.GetComponent<PostProcessingBehaviour>().profile.vignette;
        vignetteIntensity = 0;

        InitEffects();
    }

    private void InitEffects()
    {
        blur.downsample = 1;
        blur.blurSize = 0;
        blur.blurIterations = 2;
        blur.blurShader = Shader.Find("Hidden/FastBlur");

        motionBlur.blurAmount = 0;
        motionBlur.extraBlur = false;
        motionBlur.shader = Shader.Find("Hidden/MotionBlur");

        ChangeBlur(true);
        ChangeMotionBlur(true);

        SetVignetteIntensity(0);
        ChangeVignette(true);
    }

    /// <summary>
    /// Activate or deactivate blur and motion blur effect
    /// </summary>
    public void ChangeBlur(bool bActivate)
    {
        blur.enabled = bActivate;
    }

    public void SetBlurIntensity(float intensity)
    {
        blur.blurSize = intensity;
    }

    public void ChangeMotionBlur(bool bActivate)
    {
        motionBlur.enabled = bActivate;
    }

    public void SetMotionBlurIntensity(float intensity)
    {
        motionBlur.blurAmount = intensity / 10;
    }

    public IEnumerator SetBlurIntensitySmooth(float intensity)
    {
        bool increment = blur.blurSize < intensity;
        float step = 0.05f;
        float delta = Mathf.Abs(blur.blurSize - intensity);
        float steps = delta / step;
        while ((increment && blur.blurSize < intensity) || (!increment && blur.blurSize > intensity))
        {
            //TODO: make time depends of delta? 
            //TODO: make it unpossible to run a new co-routine with StartCoroutine if another is already running!
            yield return new WaitForSecondsRealtime(0.8f / steps);
            if (increment)
            {
                blur.blurSize += step;
                motionBlur.blurAmount += step / 10;
            }
            else
            {
                blur.blurSize -= step;
                motionBlur.blurAmount -= step / 10;
            }
        }

        SetBlurIntensity(intensity);
    }

    /// <summary>
    /// Activate or deactivate vignette effect
    /// </summary>
    public void ChangeVignette(bool bActivate)
    {
        vignette.enabled = bActivate;
    }

    /// <summary>
    /// Activate or deactivate pulsation vignette effect
    /// </summary>
    public void ChangeVignettePulsation(bool bActivate)
    {
        bPulsation = bActivate;
    }

    public bool IsVignettePulsation()
    {
        return bPulsation;
    }

    public void SetVignettePulsationIntensityAndFrequency(float maxIntensity, float minIntensity, float frequency)
    {
        this.maxIntensity = maxIntensity;
        this.minIntensity = minIntensity;
        this.frequency = frequency;
    }

    private IEnumerator SetVignetteIntensitySmooth(float intensity)
    {
        VignetteModel.Settings vignetteSettings = vignette.settings;

        bool increment = vignetteSettings.intensity < intensity;
        float step = 0.01f;
        float delta = Mathf.Abs(vignetteSettings.intensity - intensity);
        float steps = delta / step;
        while ((increment && vignetteSettings.intensity < intensity) || (!increment && vignetteSettings.intensity > intensity))
        {
            //TODO: make time depends of delta? 
            //TODO: make it unpossible to run a new co-routine with StartCoroutine if another is already running! 
            yield return new WaitForSecondsRealtime(0.5f / steps);
            if (increment)
            {
                vignetteSettings.intensity += step;
                //overwrite the original with copy
                vignette.settings = vignetteSettings;
            }
            else
            {
                vignetteSettings.intensity -= step;
                vignette.settings = vignetteSettings;
            }
        }

        vignetteSettings.intensity = intensity;
        vignette.settings = vignetteSettings;
    }

    public void SetVignetteIntensity(float intensity)
    {
        VignetteModel.Settings vignetteSettings = vignette.settings;
        vignetteSettings.intensity = intensity;
        vignette.settings = vignetteSettings;
    }

    public void DoVignetteIntensitySmoothPulsation(float speed)
    {
        StartCoroutine(RoutineVignetteIntensitySmoothPulsation(speed));
    }

    private IEnumerator RoutineVignetteIntensitySmoothPulsation(float speed)
    {
        //pulsation
        while (bPulsation)
        {
            StartCoroutine(Utilities.ControllerVibration(0.25f, 0.25f, 0.1f));

            var intensity = vignetteIntensity;

            if (!reachedHigh && reachedLow)
            {
                if (vignetteIntensity > maxIntensity)
                {
                    reachedHigh = true;
                    reachedLow = false;
                }
                else
                {
                    vignetteIntensity += speed;
                }
            }
            else if (!reachedLow && reachedHigh)
            {
                if (vignetteIntensity > minIntensity)
                {
                    vignetteIntensity -= speed;
                }
                else
                {
                    reachedLow = true;
                    reachedHigh = false;
                }
            }

            SetVignetteIntensity(vignetteIntensity);

            yield return null;
        }
    }
}